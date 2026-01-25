using OrderManagementSystem.Domain.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderManagementSystem.Domain.Models
{
    public class Customer : BaseEntity
    {
        [Key]
        [ForeignKey("ApplicationUser")]
        public string Id { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public ICollection<Order>? Orders { get; set; }
    }
}
