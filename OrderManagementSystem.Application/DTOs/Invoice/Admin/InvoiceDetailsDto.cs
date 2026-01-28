using OrderManagementSystem.Domain.Enums;

namespace OrderManagementSystem.Application.DTOs.Invoice.Admin
{
    public class InvoiceDetailsDto
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTimeOffset InvoiceDate { get; set; }
        public int OrderId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}