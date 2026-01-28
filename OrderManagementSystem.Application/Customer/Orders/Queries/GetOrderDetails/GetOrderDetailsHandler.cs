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

namespace OrderManagementSystem.Application.Customer.Orders.Queries.GetOrderDatails
{
    public class GetOrderDetailsHandler : IRequestHandler<GetOrderDetailsQuery, ResponseDto<OrderDetailsDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetOrderDetailsHandler> _logger;
        private readonly IMapper _mapper;

        public GetOrderDetailsHandler(IUnitOfWork unitOfWork,
                                      ILogger<GetOrderDetailsHandler> logger,
                                      IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<ResponseDto<OrderDetailsDto>> Handle(GetOrderDetailsQuery request, CancellationToken cancellationToken)
        {
            var orderRepository = _unitOfWork.Repository<Order>();
            try
            {
                var order = await orderRepository
                       .GetAllAsNoTracking()
                       .Where(o => o.Id == request.Id && o.CustomerId == request.CustomerId)
                       .ProjectTo<OrderDetailsDto>(_mapper.ConfigurationProvider)
                       .SingleOrDefaultAsync(cancellationToken);

                if (order == null)
                {
                    _logger.LogWarning(
                           "Order {OrderId} not found for customer {CustomerId}. " +
                           "Either order doesn't exist or customer doesn't have access",
                           request.Id,
                           request.CustomerId);

                    return ResponseDto<OrderDetailsDto>.Error(ErrorCode.NotFound, $" Order with ID {request.Id} not found");
                }

                _logger.LogInformation("Order with ID {OrderId} retrieved successfully", request.Id);
                return ResponseDto<OrderDetailsDto>.Success(order, $"Order with id {request.Id} retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve order with ID {OrderId}", request.Id);
                return ResponseDto<OrderDetailsDto>.Error(ErrorCode.DatabaseError, "Failed to retrieve order.");
            }
        }
    }
}