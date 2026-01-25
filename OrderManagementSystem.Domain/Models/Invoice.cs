using OrderManagementSystem.Domain.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderManagementSystem.Domain.Models
{
    public class Invoice : BaseEntity
    {
        public int Id { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTimeOffset InvoiceDate { get; set; } = DateTimeOffset.UtcNow;

        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}