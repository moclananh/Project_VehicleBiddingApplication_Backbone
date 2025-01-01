

using System.ComponentModel.DataAnnotations;

namespace BiddingApp.Infrastructure.Dtos.UserDtos
{
    public class RegisterVm
    {
        public string UserName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } // Admin, Customer
        public decimal Budget { get; set; }
    }
}
