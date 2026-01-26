namespace OrderManagementSystem.Application.DTOs.Account
{
    public class LoginResponseDto
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
        public DateTime AccessTokenExpiresAt { get; set; }
    }
}