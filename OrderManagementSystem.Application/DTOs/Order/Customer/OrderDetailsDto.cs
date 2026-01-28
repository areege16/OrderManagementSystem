using OrderManagementSystem.Application.DTOs.Invoice.Customer;
using OrderManagementSystem.Domain.Enums;

namespace OrderManagementSystem.Application.DTOs.Order.Customer
{
    public class OrderDetailsDto
    {
        public int Id { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
        public decimal TotalAmount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public ICollection<OrderItemDto> OrderItems { get; set; } = [];
        public CustomerInvoiceSummaryDto? Invoice { get; set; }
    }
}