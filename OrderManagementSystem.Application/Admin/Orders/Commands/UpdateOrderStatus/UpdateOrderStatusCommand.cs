using MediatR;
using OrderManagementSystem.Application.Common.Responses;
using OrderManagementSystem.Application.DTOs.Order.Admin;

namespace OrderManagementSystem.Application.Admin.Orders.Commands.UpdateOrderStatus
{
    public class UpdateOrderStatusCommand : IRequest<ResponseDto<bool>>
    {
        public UpdateOrderStatusDto UpdateOrderStatusDto { get; set; }
    }
}