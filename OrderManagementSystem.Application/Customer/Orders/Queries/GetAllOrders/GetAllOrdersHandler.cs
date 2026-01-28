using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OrderManagementSystem.Application.Common.Responses;
using OrderManagementSystem.Application.DTOs.Order.Customer;
using OrderManagementSystem.Application.UnitOfWorkContract;
using OrderManagementSystem.Domain.Enums;
using OrderManagementSystem.Domain.Models;

namespace OrderManagementSystem.Application.Customer.Orders.Queries.GetAllOrders
{
    public class GetAllOrdersHandler : IRequestHandler<GetAllOrdersQuery, ResponseDto<List<CustomerOrderSummaryDto>>>
    {
        private readonly ILogger<GetAllOrdersHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllOrdersHandler(ILogger<GetAllOrdersHandler> logger,
                                    IUnitOfWork unitOfWork,
                                    IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ResponseDto<List<CustomerOrderSummaryDto>>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var orderRepository = _unitOfWork.Repository<Order>();
            try
            {
                var orders = await orderRepository
                 .GetAllAsNoTracking()
                 .Where(o => o.CustomerId == request.CustomerId)
                 .OrderBy(o => o.OrderDate)
                 .ProjectTo<CustomerOrderSummaryDto>(_mapper.ConfigurationProvider)
                 .ToListAsync(cancellationToken);

                if (orders.Count == 0)
                {
                    _logger.LogWarning("No orders found.");

                    return ResponseDto<List<CustomerOrderSummaryDto>>.Error(ErrorCode.NotFound, "No orders found");
                }

                _logger.LogInformation("Retrieved {Count} orders for customer {CustomerId}", orders.Count, request.CustomerId);

                return ResponseDto<List<CustomerOrderSummaryDto>>.Success(orders, "Orders retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve orders for customer {CustomerId}", request.CustomerId);
                return ResponseDto<List<CustomerOrderSummaryDto>>.Error(ErrorCode.DatabaseError, "Failed to retrieve orders.");
            }
        }
    }
}