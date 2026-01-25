using Microsoft.AspNetCore.Identity;
using OrderManagementSystem.Application.Account;
using OrderManagementSystem.Domain.Models;

namespace OrderManagementSystem.Web.Seed
{
    public class RoleSeeder
    {
        public static async Task SeedRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            var roles = Roles.AllowedRoles;

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new ApplicationRole(role));
                }
            }
        }
    }
}