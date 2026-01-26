using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using OrderManagementSystem.Application.Common.Responses;
using OrderManagementSystem.Application.UnitOfWorkContract;
using OrderManagementSystem.Domain.Enums;
using OrderManagementSystem.Domain.Models;

namespace OrderManagementSystem.Application.Account.Commands.Register
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, ResponseDto<bool>>
    {
        private static readonly HashSet<string> AllowedRolesSet = new(Roles.AllowedRoles, StringComparer.OrdinalIgnoreCase);
        private readonly ILogger<RegisterHandler> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterHandler(ILogger<RegisterHandler> logger,
                               UserManager<ApplicationUser> userManager,
                               IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }
        public async Task<ResponseDto<bool>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var registerRequest = request.RegisterRequestDto;
            try
            {
                _logger.LogInformation("Register attempt for user {UserName} with role {Role}", registerRequest.UserName, registerRequest.Role);


                if (!string.IsNullOrEmpty(registerRequest.Role) && !AllowedRolesSet.Contains(registerRequest.Role))
                {
                    _logger.LogWarning("Invalid role specified for registration by {UserName}: {Role}", registerRequest.UserName, registerRequest.Role);
                    return ResponseDto<bool>.Error(ErrorCode.InvalidRole, "Invalid role specified. Allowed roles are: Admin, Customer");
                }

                ApplicationUser user = new ApplicationUser()
                {
                    UserName = registerRequest.UserName,
                    Name = registerRequest.Name,
                    Email = registerRequest.Email,
                };

                IdentityResult result = await _userManager.CreateAsync(user, registerRequest.Password);

                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    _logger.LogWarning("User creation failed for {UserName}: {Errors}", registerRequest.UserName, errors);
                    return ResponseDto<bool>.Error(ErrorCode.Unauthorized, $"Account creation failed: {errors}");
                }

                _logger.LogInformation("User created successfully: {UserName}", registerRequest.UserName);

                if (!string.IsNullOrEmpty(registerRequest.Role))
                {
                    var isInRole = await _userManager.IsInRoleAsync(user, registerRequest.Role);

                    if (!isInRole)
                    {
                        var addToRoleResult = await _userManager.AddToRoleAsync(user, registerRequest.Role);
                        if (!addToRoleResult.Succeeded)
                        {
                            var errors = string.Join(", ", addToRoleResult.Errors.Select(e => e.Description));
                            _logger.LogWarning("Failed to assign role {Role} to user {UserName}. Errors: {Errors}", registerRequest.Role, registerRequest.UserName, errors);
                            return ResponseDto<bool>.Error(ErrorCode.Unauthorized, $"Failed to assign role {errors}");
                        }
                        _logger.LogInformation("Role {Role} assigned to user {UserName}", registerRequest.Role, registerRequest.UserName);
                    }

                    switch (registerRequest.Role.ToLower())
                    {
                        case "admin":
                            var admin = new Admin { Id = user.Id };
                            _unitOfWork.Repository<Admin>().Add(admin);
                            await _unitOfWork.SaveChangesAsync(cancellationToken);
                            break;

                        case "customer":
                            var customer = new Customer { Id = user.Id };
                            _unitOfWork.Repository<Customer>().Add(customer);
                            await _unitOfWork.SaveChangesAsync(cancellationToken);
                            break;
                    }
                }
                return ResponseDto<bool>.Success(true, "Account Created successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred during registration for {UserName}", registerRequest.UserName);
                return ResponseDto<bool>.Error(ErrorCode.InternalServerError, "An unexpected error occurred during registration.");
            }
        }
    }
}