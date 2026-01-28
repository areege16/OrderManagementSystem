using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Application.Admin.Invoices.Queries.GetAllInvoices;
using OrderManagementSystem.Application.Admin.Invoices.Queries.GetInvoiceDetails;

namespace OrderManagementSystem.Web.Controllers.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class InvoicesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public InvoicesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #region GetInvoiceDetails
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInvoiceDetails(int id)
        {
            var result = await _mediator.Send(new GetInvoiceDetailsQuery
            {
                Id = id
            });
            return Ok(result);
        }
        #endregion

        #region GetInvoiceSummary
        [HttpGet]
        public async Task<IActionResult> GetInvoiceSummary()
        {
            var result = await _mediator.Send(new GetAllInvoicesQuery());
            return Ok(result);
        }
        #endregion
    }
}