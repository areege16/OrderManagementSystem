using MediatR;
using Microsoft.Extensions.Logging;
using OrderManagementSystem.Application.Abstractions;
using OrderManagementSystem.Application.Common.Responses;
using OrderManagementSystem.Application.UnitOfWorkContract;
using OrderManagementSystem.Domain.Enums;
using OrderManagementSystem.Domain.Models;

namespace OrderManagementSystem.Application.Admin.Orders.Commands.UpdateOrderStatus
{
    public class UpdateOrderStatusHandler : IRequestHandler<UpdateOrderStatusCommand, ResponseDto<bool>>
    {
        private readonly ILogger<UpdateOrderStatusHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderStatusValidationService _orderStatusValidationService;

        public UpdateOrderStatusHandler(ILogger<UpdateOrderStatusHandler> logger,
                                        IUnitOfWork unitOfWork,
                                        IOrderStatusValidationService orderStatusValidationService)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _orderStatusValidationService = orderStatusValidationService;
        }
        public async Task<ResponseDto<bool>> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var updatedOrderStatusRequest = request.UpdateOrderStatusDto;
            var orderRepository = _unitOfWork.Repository<Order>();

            try
            {
                var order = await orderRepository.GetByIdAsync(updatedOrderStatusRequest.OrderId, cancellationToken);

                if (order == null)
                {
                    _logger.LogWarning("Admin attempted to update status for non-existent order  {OrderId}", updatedOrderStatusRequest.OrderId);
                    return ResponseDto<bool>.Error(ErrorCode.NotFound, $"Order with ID {updatedOrderStatusRequest.OrderId} not found. Status update failed.");
                }

                if (order.OrderStatus == updatedOrderStatusRequest.OrderStatus)
                {
                    return ResponseDto<bool>.Success(true, "Order status is already up to date.");
                }

                if (!_orderStatusValidationService.CanTransition(order.OrderStatus, updatedOrderStatusRequest.OrderStatus))
                {
                    _logger.LogWarning("Invalid status transition from {OldStatus} to {NewStatus} for order {OrderId}", order.OrderStatus, updatedOrderStatusRequest.OrderStatus, order.Id);

                    return ResponseDto<bool>.Error(ErrorCode.ValidationFailed, $"Invalid status transition from {order.OrderStatus} to {updatedOrderStatusRequest.OrderStatus}.");
                }

                var oldStatus = order.OrderStatus;

                order.OrderStatus = updatedOrderStatusRequest.OrderStatus;
                order.UpdatedAt = DateTimeOffset.UtcNow;

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Status updated successfully for order '{OrderId}'. Previous: {OldStatus}, New: {NewStatus}", order.Id, oldStatus, order.OrderStatus);
                return ResponseDto<bool>.Success(true, "Order status updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Admin failed to update status for order {OrderId}", updatedOrderStatusRequest.OrderId);

                return ResponseDto<bool>.Error(ErrorCode.DatabaseError, "Failed to update order status");
            }
        }
    }
}