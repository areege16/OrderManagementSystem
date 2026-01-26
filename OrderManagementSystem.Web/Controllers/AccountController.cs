using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Application.Account.Commands.Login;
using OrderManagementSystem.Application.Account.Commands.Register;
using OrderManagementSystem.Application.DTOs.Account;

namespace OrderManagementSystem.Web.Controllers
{
    [Route("api/auth/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #region Register
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterRequestDto registerRequestDto)
        {
            var result = await _mediator.Send(new RegisterCommand
            {
                RegisterRequestDto = registerRequestDto
            });
            return Ok(result);
        }
        #endregion
        #region Login
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
        {
            var result = await _mediator.Send(new LoginCommand
            {
                LoginRequestDto = loginRequestDto
            });
            return Ok(result);
        }
        #endregion
    }
}