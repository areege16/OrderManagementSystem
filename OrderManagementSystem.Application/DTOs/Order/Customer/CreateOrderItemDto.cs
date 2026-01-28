namespace OrderManagementSystem.Application.DTOs.Order.Customer
{
    public class CreateOrderItemDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}