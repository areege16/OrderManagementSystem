using OrderManagementSystem.Domain.Models.Base;

namespace OrderManagementSystem.Domain.Models
{
    public class Product : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}