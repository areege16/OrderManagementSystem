using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OrderManagementSystem.Application.Common.Responses;
using OrderManagementSystem.Application.UnitOfWorkContract;
using OrderManagementSystem.Domain.Enums;
using OrderManagementSystem.Domain.Models;

namespace OrderManagementSystem.Application.Admin.Products.Commands.UpdateProduct
{
    class UpdateProductHandler : IRequestHandler<UpdateProductCommand, ResponseDto<bool>>
    {
        private readonly ILogger<UpdateProductHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateProductHandler(ILogger<UpdateProductHandler> logger,
                                     IUnitOfWork unitOfWork,
                                     IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ResponseDto<bool>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var updatedProductRequest = request.UpdateProductDto;
            var productRepository = _unitOfWork.Repository<Product>();
            try
            {
                var product = await productRepository.GetByIdAsync(updatedProductRequest.Id, cancellationToken);

                if (product == null)
                {
                    _logger.LogWarning("Admin  tried to update non-existent product {ProductId}", updatedProductRequest.Id);
                    return ResponseDto<bool>.Error(ErrorCode.NotFound, $"Product with id {updatedProductRequest.Id} not found.");
                }

                _mapper.Map(updatedProductRequest, product);
                product.UpdatedAt = DateTimeOffset.UtcNow;

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return ResponseDto<bool>.Success(true, "Product information updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Admin failed to update product {ProductId}", updatedProductRequest.Id);

                return ResponseDto<bool>.Error(ErrorCode.DatabaseError, "Failed to update product");
            }
        }
    }
}