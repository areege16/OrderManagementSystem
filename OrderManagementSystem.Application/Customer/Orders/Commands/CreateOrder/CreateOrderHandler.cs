using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OrderManagementSystem.Application.Common.Responses;
using OrderManagementSystem.Application.UnitOfWorkContract;
using OrderManagementSystem.Domain.Enums;
using OrderManagementSystem.Domain.Models;

namespace OrderManagementSystem.Application.Customer.Orders.Commands.CreateOrder
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, ResponseDto<bool>> //TODO :add helper method
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateOrderHandler> _logger;

        public CreateOrderHandler(IUnitOfWork unitOfWork, ILogger<CreateOrderHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<ResponseDto<bool>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderRepository = _unitOfWork.Repository<Order>();
            var productRepository = _unitOfWork.Repository<Product>();
            var createOrderRequest = request.CreateOrderDto;
            try
            {
                var productIds = createOrderRequest.OrderItems
                    .Select(x => x.ProductId)
                    .ToList();

                var products = await productRepository
                    .GetFiltered(p => productIds.Contains(p.Id), asTracking: true)
                    .ToListAsync(cancellationToken);

                var productDict = products.ToDictionary(p => p.Id);

                var orderItems = new List<OrderItem>();
                decimal subtotal = 0;

                foreach (var itemDto in createOrderRequest.OrderItems)
                {
                    if (!productDict.TryGetValue(itemDto.ProductId, out var product))
                    {
                        return ResponseDto<bool>.Error(ErrorCode.NotFound, $"Product with ID {itemDto.ProductId} not found.");
                    }

                    if (product.Stock < itemDto.Quantity)
                    {
                        return ResponseDto<bool>.Error(ErrorCode.ValidationFailed, $"Insufficient stock for product {product.Id}. Available: {product.Stock}, Requested: {itemDto.Quantity}");
                    }
                    product.Stock -= itemDto.Quantity;

                    orderItems.Add(new OrderItem
                    {
                        ProductId = itemDto.ProductId,
                        Quantity = itemDto.Quantity,
                        UnitPrice = product.Price,
                        Discount = 0
                    });

                    subtotal += itemDto.Quantity * product.Price;
                }

                decimal discountPercentage = subtotal switch
                {
                    >= 200 => 10,
                    >= 100 => 5,
                    _ => 0
                };

                foreach (var item in orderItems)
                {
                    item.Discount = discountPercentage;
                }

                var totalAmount = orderItems.Sum(oi => oi.Quantity * oi.UnitPrice * (1 - oi.Discount / 100m));

                var order = new Order
                {
                    CustomerId = request.CustomerId,
                    PaymentMethod = createOrderRequest.PaymentMethod,
                    OrderStatus = OrderStatus.Pending,
                    OrderDate = DateTimeOffset.UtcNow,
                    TotalAmount = totalAmount,
                    OrderItems = orderItems
                };

                orderRepository.Add(order);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return ResponseDto<bool>.Success(true, "Order Create successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Customer {CustomerId} failed to create order.", request.CustomerId);

                return ResponseDto<bool>.Error(ErrorCode.DatabaseError, "Failed to create order");
            }
        }
    }
}