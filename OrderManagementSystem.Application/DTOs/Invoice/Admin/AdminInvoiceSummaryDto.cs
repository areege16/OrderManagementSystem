namespace OrderManagementSystem.Application.DTOs.Invoice.Admin
{
    public class AdminInvoiceSummaryDto
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public int OrderId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTimeOffset InvoiceDate { get; set; }
    }
}