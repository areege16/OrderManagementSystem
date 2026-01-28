using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using OrderManagementSystem.Application.Abstractions;
using OrderManagementSystem.Application.Setting;

namespace OrderManagementSystem.Infrastructure.Services
{
    public class MailKitEmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        public MailKitEmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }
        public async Task SendOrderStatusEmailAsync(string toEmail, string toName, string orderNumber, string newStatus)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail));
            email.To.Add(new MailboxAddress(toName, toEmail));
            email.Subject = $"Order #{orderNumber} Status Updated";

            var bodyBuilder = new BodyBuilder
            {
                TextBody = $"Hello {toName},\n\nYour order #{orderNumber} status has been updated to: {newStatus}.\n\nThank you for shopping with us!"
            };
            email.Body = bodyBuilder.ToMessageBody();

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.SmtpPort, MailKit.Security.SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_emailSettings.SenderEmail, _emailSettings.SenderPassword);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}