using OrderManagementSystem.Domain.Models;

namespace OrderManagementSystem.Application.Abstractions
{
    public interface ITokenService
    {
        string GenerateAccessToken(ApplicationUser user, IList<string> roles);
    }
}