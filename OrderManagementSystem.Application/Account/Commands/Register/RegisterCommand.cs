using MediatR;
using OrderManagementSystem.Application.Common.Responses;
using OrderManagementSystem.Application.DTOs.Account;

namespace OrderManagementSystem.Application.Account.Commands.Register
{
    public class RegisterCommand : IRequest<ResponseDto<bool>>
    {
        public RegisterRequestDto RegisterRequestDto { get; set; }
    }
}