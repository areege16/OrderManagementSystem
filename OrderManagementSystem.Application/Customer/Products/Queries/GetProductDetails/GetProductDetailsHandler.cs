using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OrderManagementSystem.Application.Common.Responses;
using OrderManagementSystem.Application.DTOs.Product.Customer;
using OrderManagementSystem.Application.UnitOfWorkContract;
using OrderManagementSystem.Domain.Enums;
using OrderManagementSystem.Domain.Models;

namespace OrderManagementSystem.Application.Customer.Products.Queries.GetProductDetails
{
    public class GetProductDetailsHandler : IRequestHandler<GetProductDetailsQuery, ResponseDto<GetProductDetailsDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetProductDetailsHandler> _logger;
        private readonly IMapper _mapper;

        public GetProductDetailsHandler(IUnitOfWork unitOfWork,
                                        ILogger<GetProductDetailsHandler> logger,
                                        IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<ResponseDto<GetProductDetailsDto>> Handle(GetProductDetailsQuery request, CancellationToken cancellationToken)
        {
            var productRepository = _unitOfWork.Repository<Product>();
            try
            {
                var product = await productRepository
                    .GetFiltered(p => p.Id == request.Id, asTracking: false)
                    .ProjectTo<GetProductDetailsDto>(_mapper.ConfigurationProvider)
                    .SingleOrDefaultAsync(cancellationToken);

                if (product == null)
                {
                    _logger.LogWarning("Product with ID {ProductId} not found", request.Id);
                    return ResponseDto<GetProductDetailsDto>.Error(ErrorCode.NotFound, $" Product with ID {request.Id} not found");
                }

                _logger.LogInformation("Product with ID {ProductId} retrieved successfully", request.Id);
                return ResponseDto<GetProductDetailsDto>.Success(product, $"Product with id {request.Id} retrieved successfully");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve product with ID {ProductId}", request.Id);
                return ResponseDto<GetProductDetailsDto>.Error(ErrorCode.DatabaseError, "Failed to retrieve product.");
            }
        }
    }
}