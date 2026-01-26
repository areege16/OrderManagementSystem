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

namespace OrderManagementSystem.Application.Customer.Products.Queries.GetAllProducts
{

    class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, ResponseDto<List<GetAllProductsDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetAllProductsHandler> _logger;
        private readonly IMapper _mapper;

        public GetAllProductsHandler(IUnitOfWork unitOfWork,
                                     ILogger<GetAllProductsHandler> logger,
                                     IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<ResponseDto<List<GetAllProductsDto>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var productRepository = _unitOfWork.Repository<Product>();
            try
            {
                var products = await productRepository
                    .GetAllAsNoTracking()
                    .ProjectTo<GetAllProductsDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                if (products.Count == 0)
                {
                    _logger.LogWarning("No products found.");

                    return ResponseDto<List<GetAllProductsDto>>.Error(ErrorCode.NotFound, "No products found");
                }

                return ResponseDto<List<GetAllProductsDto>>.Success(products, "Products retrieved successfully");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving products.");

                return ResponseDto<List<GetAllProductsDto>>.Error(ErrorCode.DatabaseError, "Failed to retrieve products.");
            }
        }
    }
}