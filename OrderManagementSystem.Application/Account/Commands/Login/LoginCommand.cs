using MediatR;
using OrderManagementSystem.Application.Common.Responses;
using OrderManagementSystem.Application.DTOs.Account;

namespace OrderManagementSystem.Application.Account.Commands.Login
{
    public class LoginCommand : IRequest<ResponseDto<LoginResponseDto>>
    {
        public LoginRequestDto LoginRequestDto { get; set; }
    }
}