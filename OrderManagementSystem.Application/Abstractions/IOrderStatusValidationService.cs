using OrderManagementSystem.Domain.Enums;

namespace OrderManagementSystem.Application.Abstractions
{
    public interface IOrderStatusValidationService
    {
        bool CanTransition(OrderStatus current, OrderStatus next);
    }
}