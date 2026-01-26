using MediatR;
using OrderManagementSystem.Application.Common.Responses;
using OrderManagementSystem.Application.DTOs.Product.Customer;

namespace OrderManagementSystem.Application.Customer.Products.Queries.GetProductDetails
{
    public class GetProductDetailsQuery : IRequest<ResponseDto<GetProductDetailsDto>>
    {
        public int Id { get; set; }
    }
}