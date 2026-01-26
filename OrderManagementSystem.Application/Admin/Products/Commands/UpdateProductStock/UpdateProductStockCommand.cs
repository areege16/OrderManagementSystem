using MediatR;
using OrderManagementSystem.Application.Common.Responses;
using OrderManagementSystem.Application.DTOs.Product.Admin;

namespace OrderManagementSystem.Application.Admin.Products.Commands.UpdateProductStock
{
    public class UpdateProductStockCommand : IRequest<ResponseDto<bool>>
    {
        public UpdateProductStockDto UpdateProductStockDto { get; set; }
    }
}