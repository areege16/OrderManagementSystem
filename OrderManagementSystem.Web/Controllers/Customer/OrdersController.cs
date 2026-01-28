using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Application.Customer.Orders.Commands.CreateOrder;
using OrderManagementSystem.Application.Customer.Orders.Queries.GetAllOrders;
using OrderManagementSystem.Application.Customer.Orders.Queries.GetOrderDatails;
using OrderManagementSystem.Application.DTOs.Order.Customer;
using OrderManagementSystem.Web.Extensions;

namespace OrderManagementSystem.Web.Controllers.Customer
{
    [Route("api/customer/[controller]")]
    [ApiController]
    [Authorize(Roles ="Customer")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #region CreateOrder
        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderDto createOrderDto)
        {
            var result = await _mediator.Send(new CreateOrderCommand
            {
                CustomerId = User.GetUserId(),
                CreateOrderDto = createOrderDto
            });
            return Ok(result);
        }
        #endregion

        #region GetOrderDetails
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderDetails(int id)
        {
            var result = await _mediator.Send(new GetOrderDetailsQuery
            {
                CustomerId = User.GetUserId(),
                Id = id
            });
            return Ok(result);
        }
        #endregion

        #region GetCustomerOrderSummary
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var result = await _mediator.Send(new GetAllOrdersQuery
            {
                CustomerId = User.GetUserId(),
            });
            return Ok(result);
        }
        #endregion
    }
}