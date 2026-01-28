using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OrderManagementSystem.Application.Common.Responses;
using OrderManagementSystem.Application.UnitOfWorkContract;
using OrderManagementSystem.Domain.Enums;
using OrderManagementSystem.Domain.Models;

namespace OrderManagementSystem.Application.Admin.Products.Commands.CreateProduct
{
    class CreateProductHandler : IRequestHandler<CreateProductCommand, ResponseDto<bool>>
    {
        private readonly ILogger<CreateProductHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateProductHandler(ILogger<CreateProductHandler> logger,
                                    IUnitOfWork unitOfWork,
                                    IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ResponseDto<bool>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var createProductRequest = request.CreateProductDto;
            try
            {
                var product = _mapper.Map<Product>(createProductRequest);
                _unitOfWork.Repository<Product>().Add(product);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return ResponseDto<bool>.Success(true, "Product added successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Admin failed to add product {ProductName}.", createProductRequest.Name);

                return ResponseDto<bool>.Error(ErrorCode.DatabaseError, "Failed to add product");
            }
        }
    }
}