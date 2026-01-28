namespace OrderManagementSystem.Application.Abstractions
{
    public interface IEmailService
    {
        Task SendOrderStatusEmailAsync(string toEmail, string toName, string orderNumber, string newStatus);
    }
}