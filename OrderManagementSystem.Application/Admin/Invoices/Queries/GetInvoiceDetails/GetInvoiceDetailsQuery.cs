using MediatR;
using OrderManagementSystem.Application.Common.Responses;
using OrderManagementSystem.Application.DTOs.Invoice.Admin;

namespace OrderManagementSystem.Application.Admin.Invoices.Queries.GetInvoiceDetails
{
    public class GetInvoiceDetailsQuery : IRequest<ResponseDto<InvoiceDetailsDto>>
    {
        public int Id { get; set; }
    }
}