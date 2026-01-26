using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Application.Customer.Products.Queries.GetAllProducts;
using OrderManagementSystem.Application.Customer.Products.Queries.GetProductDetails;

namespace OrderManagementSystem.Web.Controllers.Customer
{
    [Route("api/Customer/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly IMediator _mediator;
        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #region GetAllProducts
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var result = await _mediator.Send(new GetAllProductsQuery());
            return Ok(result);
        }
        #endregion

        #region GetProductDetails
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductDetails(int id)
        {
            var result = await _mediator.Send(new GetProductDetailsQuery
            {
                Id = id
            });
            return Ok(result);
        }
        #endregion
    }
}