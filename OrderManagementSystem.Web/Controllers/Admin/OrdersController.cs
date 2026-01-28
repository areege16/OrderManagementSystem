using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Application.Admin.Orders.Commands.UpdateOrderStatus;
using OrderManagementSystem.Application.Admin.Orders.Queries.GetAllOrders;
using OrderManagementSystem.Application.DTOs.Order.Admin;

namespace OrderManagementSystem.Web.Controllers.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #region UpdateOrderStatus
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateOrderStatus(int id, UpdateOrderStatusDto updateOrderStatusDto)
        {
            updateOrderStatusDto.OrderId = id;
            var result = await _mediator.Send(new UpdateOrderStatusCommand
            {
                UpdateOrderStatusDto = updateOrderStatusDto,
            });
            return Ok(result);
        }
        #endregion

        #region GetAdminOrderSummary 
        [HttpGet]
        public async Task<IActionResult> GetAllOrdersQuery()
        {
            var result = await _mediator.Send(new GetAllOrdersQuery());
            return Ok(result);
        }
        #endregion
    }
}
