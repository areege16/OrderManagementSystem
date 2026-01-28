using MediatR;
using OrderManagementSystem.Application.Common.Responses;
using OrderManagementSystem.Application.DTOs.Order.Customer;

namespace OrderManagementSystem.Application.Customer.Orders.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<ResponseDto<bool>>
    {
        public string CustomerId { get; set; }
        public CreateOrderDto CreateOrderDto { get; set; }
    }
}