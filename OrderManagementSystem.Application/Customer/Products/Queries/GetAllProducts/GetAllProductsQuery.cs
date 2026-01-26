using MediatR;
using OrderManagementSystem.Application.Common.Responses;
using OrderManagementSystem.Application.DTOs.Product.Customer;

namespace OrderManagementSystem.Application.Customer.Products.Queries.GetAllProducts
{
    public class GetAllProductsQuery : IRequest<ResponseDto<List<GetAllProductsDto>>>
    {
    }
}