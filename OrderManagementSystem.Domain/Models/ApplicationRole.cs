using Microsoft.AspNetCore.Identity;

namespace OrderManagementSystem.Domain.Models
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() { }
        public ApplicationRole(string roleName) : base(roleName)
        {
        }
    }
}