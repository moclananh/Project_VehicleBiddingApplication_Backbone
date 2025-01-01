using BiddingApp.Domain.Models.Enums;

namespace BiddingApp.Infrastructure.Dtos.UserDtos
{
    public class LoginResponse
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public UserRole Role { get; set; }
        public decimal Budget { get; set; }
        public string Token { get; set; }
    }
}
