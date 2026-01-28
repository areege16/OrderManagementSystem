using OrderManagementSystem.Domain.Enums;

namespace OrderManagementSystem.Application.DTOs.Order.Admin
{
    public class AdminOrderSummaryDto
    {
        public int Id { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
        public decimal TotalAmount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
    }
}