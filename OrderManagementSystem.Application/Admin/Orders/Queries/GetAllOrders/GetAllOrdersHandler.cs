using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OrderManagementSystem.Application.Common.Responses;
using OrderManagementSystem.Application.DTOs.Order.Admin;
using OrderManagementSystem.Application.UnitOfWorkContract;
using OrderManagementSystem.Domain.Enums;
using OrderManagementSystem.Domain.Models;

namespace OrderManagementSystem.Application.Admin.Orders.Queries.GetAllOrders
{
    public class GetAllOrdersHandler : IRequestHandler<GetAllOrdersQuery, ResponseDto<List<AdminOrderSummaryDto>>>
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
        public async Task<ResponseDto<List<AdminOrderSummaryDto>>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var orderRepository = _unitOfWork.Repository<Order>();
            try
            {
                var orders = await orderRepository
                 .GetAllAsNoTracking()
                 .OrderBy(o => o.OrderDate)
                 .ProjectTo<AdminOrderSummaryDto>(_mapper.ConfigurationProvider)
                 .ToListAsync(cancellationToken);

                if (orders.Count == 0)
                {
                    _logger.LogWarning("No orders found.");

                    return ResponseDto<List<AdminOrderSummaryDto>>.Error(ErrorCode.NotFound, "No orders found");
                }

                _logger.LogInformation("Retrieved {Count} orders for admin", orders.Count);

                return ResponseDto<List<AdminOrderSummaryDto>>.Success(orders, "Orders retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve orders.");

                return ResponseDto<List<AdminOrderSummaryDto>>.Error(ErrorCode.DatabaseError, "Failed to retrieve orders.");
            }
        }
    }
}