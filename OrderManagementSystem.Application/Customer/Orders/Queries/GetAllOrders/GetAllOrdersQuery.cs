using MediatR;
using OrderManagementSystem.Application.Common.Responses;
using OrderManagementSystem.Application.DTOs.Order.Customer;

namespace OrderManagementSystem.Application.Customer.Orders.Queries.GetAllOrders
{
    public class GetAllOrdersQuery : IRequest<ResponseDto<List<CustomerOrderSummaryDto>>>
    {
        public string CustomerId { get; set; }
    }
}