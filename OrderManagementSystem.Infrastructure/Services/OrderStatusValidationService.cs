using OrderManagementSystem.Application.Abstractions;
using OrderManagementSystem.Domain.Enums;

namespace OrderManagementSystem.Infrastructure.Services
{
    public class OrderStatusValidationService : IOrderStatusValidationService
    {
        public bool CanTransition(OrderStatus current, OrderStatus next)
        {
            return (current, next) switch
            {
                (OrderStatus.Pending, OrderStatus.Confirmed) => true,
                (OrderStatus.Pending, OrderStatus.Cancelled) => true,

                (OrderStatus.Confirmed, OrderStatus.Processing) => true,
                (OrderStatus.Confirmed, OrderStatus.Cancelled) => true,

                (OrderStatus.Processing, OrderStatus.Shipped) => true,
                (OrderStatus.Processing, OrderStatus.Cancelled) => true,

                (OrderStatus.Shipped, OrderStatus.Delivered) => true,
                (OrderStatus.Shipped, OrderStatus.Cancelled) => true,

                (OrderStatus.Delivered, _) => false,
                (OrderStatus.Cancelled, _) => false,

                (_, _) when current == next => true,

                _ => false
            };
        }
    }
}