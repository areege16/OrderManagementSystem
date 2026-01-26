namespace OrderManagementSystem.Application.DTOs.Product.Admin
{
    public class CreateProductDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}