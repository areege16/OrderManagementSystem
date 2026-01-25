using Microsoft.AspNetCore.Identity;

namespace OrderManagementSystem.Domain.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }
        public Customer? Customer { get; set; }
        public Admin? Admin { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    }
}