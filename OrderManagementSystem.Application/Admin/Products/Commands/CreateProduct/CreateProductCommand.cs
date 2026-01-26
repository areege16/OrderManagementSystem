using MediatR;
using OrderManagementSystem.Application.Common.Responses;
using OrderManagementSystem.Application.DTOs.Product.Admin;

namespace OrderManagementSystem.Application.Admin.Products.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<ResponseDto<bool>>
    {
        public CreateProductDto CreateProductDto { get; set; }
    }
}