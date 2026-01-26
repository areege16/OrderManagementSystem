using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Application.Admin.Products.Commands.CreateProduct;
using OrderManagementSystem.Application.Admin.Products.Commands.UpdateProduct;
using OrderManagementSystem.Application.Admin.Products.Commands.UpdateProductStock;
using OrderManagementSystem.Application.DTOs.Product.Admin;

namespace OrderManagementSystem.Web.Controllers.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #region CreateProduct
        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
        {
            var result = await _mediator.Send(new CreateProductCommand
            {
                CreateProductDto = createProductDto,
            });
            return Ok(result);
        }
        #endregion

        #region UpdateProduct
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id , UpdateProductDto updateProductDto)
        {
            updateProductDto.Id = id;
            var result = await _mediator.Send(new UpdateProductCommand
            {
                UpdateProductDto = updateProductDto,
            });
            return Ok(result);
        }
        #endregion

        #region UpdateProductStock
        [HttpPatch("{id}/stock")]
        public async Task<IActionResult> UpdateProductStock(int id, UpdateProductStockDto updateProductStockDto)
        {
            updateProductStockDto.Id = id;
            var result = await _mediator.Send(new UpdateProductStockCommand
            {
                UpdateProductStockDto = updateProductStockDto,
            });
            return Ok(result);
        }
        #endregion
    }
}