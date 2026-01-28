using MediatR;
using OrderManagementSystem.Application.Common.Responses;
using OrderManagementSystem.Application.DTOs.Invoice.Admin;

namespace OrderManagementSystem.Application.Admin.Invoices.Queries.GetAllInvoices
{
    public class GetAllInvoicesQuery : IRequest<ResponseDto<List<AdminInvoiceSummaryDto>>>
    {
    }
}