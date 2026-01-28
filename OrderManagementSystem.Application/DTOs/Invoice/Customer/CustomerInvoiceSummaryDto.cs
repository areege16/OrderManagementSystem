namespace OrderManagementSystem.Application.DTOs.Invoice.Customer
{
    public class CustomerInvoiceSummaryDto
    {
        public int Id { get; set; }
        public DateTimeOffset InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }
    }
}