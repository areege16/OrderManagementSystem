namespace OrderManagementSystem.Application.DTOs.Product.Customer
{
    public class GetAllProductsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}