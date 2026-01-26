using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OrderManagementSystem.Application.Abstractions;
using OrderManagementSystem.Application.Common.Responses;
using OrderManagementSystem.Application.DTOs.Account;
using OrderManagementSystem.Application.Setting;
using OrderManagementSystem.Domain.Enums;
using OrderManagementSystem.Domain.Models;

namespace OrderManagementSystem.Application.Account.Commands.Login
{
    class LoginHandler : IRequestHandler<LoginCommand, ResponseDto<LoginResponseDto>>
    {
        private readonly ILogger<LoginHandler> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly JwtSettings _jwtSettings;

        public LoginHandler(ILogger<LoginHandler> logger,
                            UserManager<ApplicationUser> userManager,
                            ITokenService tokenService,
                            IOptions<JwtSettings> jwtSettings)
        {
            _logger = logger;
            _userManager = userManager;
            _tokenService = tokenService;
            _jwtSettings = jwtSettings.Value;
        }
        public async Task<ResponseDto<LoginResponseDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var loginRequest = request.LoginRequestDto;
            try
            {
                _logger.LogInformation("Login attempt for user {UserName}", loginRequest.UserName);

                ApplicationUser user = await _userManager.FindByNameAsync(loginRequest.UserName);

                if (user == null)
                {
                    _logger.LogWarning("Login failed: user not found {UserName}", loginRequest.UserName);
                    return ResponseDto<LoginResponseDto>.Error(ErrorCode.NotFound, "User not found");
                }

                bool isPasswordCorrect = await _userManager.CheckPasswordAsync(user, loginRequest.Password);
                if (!isPasswordCorrect)
                {
                    _logger.LogWarning("Login failed: invalid password for {UserName}", loginRequest.UserName);
                    return ResponseDto<LoginResponseDto>.Error(ErrorCode.Unauthorized, "Invalid username or password");
                }
                var userRoles = await _userManager.GetRolesAsync(user);
                var accessToken = _tokenService.GenerateAccessToken(user, userRoles);

                _logger.LogInformation("Login successful for user {UserName}", loginRequest.UserName);

                var loginDto = new LoginResponseDto
                {
                    UserName = user.UserName,
                    Name = user.Name,
                    Role = userRoles.FirstOrDefault(),
                    Token = accessToken,
                    AccessTokenExpiresAt = DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes),
                };
                return ResponseDto<LoginResponseDto>.Success(loginDto, "Login successful");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred during login for {UserName}", loginRequest.UserName);
                return ResponseDto<LoginResponseDto>.Error(ErrorCode.InternalServerError, "An unexpected error occurred during login.");
            }
        }
    }
}