using MediatR;
using OrderManagementSystem.Application.Common.Responses;
using OrderManagementSystem.Application.DTOs.Product.Customer;

namespace OrderManagementSystem.Application.Customer.Products.Queries.GetProductDetails
{
    public class GetProductDetailsQuery : IRequest<ResponseDto<ProductDetailsDto>>
    {
        public int Id { get; set; }
    }
}