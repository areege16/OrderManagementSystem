using MediatR;
using OrderManagementSystem.Application.Common.Responses;
using OrderManagementSystem.Application.DTOs.Order.Customer;

namespace OrderManagementSystem.Application.Customer.Orders.Queries.GetOrderDatails
{
    public class GetOrderDetailsQuery : IRequest<ResponseDto<OrderDetailsDto>>
    {
        public int Id { get; set; }
        public string CustomerId { get; set; }
    }
}