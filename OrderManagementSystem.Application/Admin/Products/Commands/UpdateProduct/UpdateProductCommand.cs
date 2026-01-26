using MediatR;
using OrderManagementSystem.Application.Common.Responses;
using OrderManagementSystem.Application.DTOs.Product.Admin;

namespace OrderManagementSystem.Application.Admin.Products.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest<ResponseDto<bool>>
    {
        public UpdateProductDto UpdateProductDto { get; set; }
    }
}