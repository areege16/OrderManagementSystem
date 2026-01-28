using OrderManagementSystem.Domain.Enums;
using OrderManagementSystem.Domain.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderManagementSystem.Domain.Models
{
    public class Order : BaseEntity
    {
        public int Id { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
        public decimal TotalAmount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public OrderStatus OrderStatus { get; set; }

        [ForeignKey("Customer")]
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
        public Invoice? Invoice { get; set; }
        public ICollection<OrderItem>? OrderItems { get; set; }
    }
}