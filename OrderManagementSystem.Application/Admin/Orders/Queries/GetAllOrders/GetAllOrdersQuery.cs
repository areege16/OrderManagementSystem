using MediatR;
using OrderManagementSystem.Application.Common.Responses;
using OrderManagementSystem.Application.DTOs.Order.Admin;

namespace OrderManagementSystem.Application.Admin.Orders.Queries.GetAllOrders
{
    public class GetAllOrdersQuery : IRequest<ResponseDto<List<AdminOrderSummaryDto>>>
    {
    }
}