using OrderManagementSystem.Domain.Enums;

namespace OrderManagementSystem.Application.DTOs.Order.Customer
{
    public class CustomerOrderSummaryDto
    {
        public int Id { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
        public decimal TotalAmount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}