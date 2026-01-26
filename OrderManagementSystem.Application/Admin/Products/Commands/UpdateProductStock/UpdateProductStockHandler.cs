using MediatR;
using Microsoft.Extensions.Logging;
using OrderManagementSystem.Application.Common.Responses;
using OrderManagementSystem.Application.UnitOfWorkContract;
using OrderManagementSystem.Domain.Enums;
using OrderManagementSystem.Domain.Models;

namespace OrderManagementSystem.Application.Admin.Products.Commands.UpdateProductStock
{
    public class UpdateProductStockHandler : IRequestHandler<UpdateProductStockCommand, ResponseDto<bool>>
    {
        private readonly ILogger<UpdateProductStockHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProductStockHandler(ILogger<UpdateProductStockHandler> logger, IUnitOfWork unitOfWork)

        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        public async Task<ResponseDto<bool>> Handle(UpdateProductStockCommand request, CancellationToken cancellationToken)
        {
            var updatedProductStockRequest = request.UpdateProductStockDto;
            var productRepository = _unitOfWork.Repository<Product>();
            try
            {
                var product = await productRepository.GetByIdAsync(updatedProductStockRequest.Id, cancellationToken);

                if (product == null)
                {
                    _logger.LogWarning("Admin attempted to update stock for non-existent product {ProductId}", updatedProductStockRequest.Id);
                    return ResponseDto<bool>.Error(ErrorCode.NotFound, $"Product with ID {updatedProductStockRequest.Id} not found. Stock update failed.");
                }

                var oldStock = product.Stock;

                product.Stock = updatedProductStockRequest.Stock;
                product.UpdatedAt = DateTimeOffset.UtcNow;

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Stock updated successfully for product '{ProductName}'. Previous: {OldStock}, New: {NewStock}", product.Name, oldStock, product.Stock);
                return ResponseDto<bool>.Success(true, "Product stock updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Admin failed to update stock for product {ProductId}", updatedProductStockRequest.Id);

                return ResponseDto<bool>.Error(ErrorCode.DatabaseError, "Failed to update product stock");
            }
        }
    }
}