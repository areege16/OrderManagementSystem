using OrderManagementSystem.Domain.Enums;

namespace OrderManagementSystem.Application.DTOs.Order.Admin
{
    public class UpdateOrderStatusDto
    {
        public int OrderId { set; get; }
        public OrderStatus OrderStatus { set; get; }
    }
}