using OrderManagementSystem.Domain.Enums;

namespace OrderManagementSystem.Application.DTOs.Order.Customer
{
    public class CreateOrderDto
    {
        public PaymentMethod PaymentMethod { get; set; }
        public ICollection<CreateOrderItemDto> OrderItems { get; set; } = [];
    }
}