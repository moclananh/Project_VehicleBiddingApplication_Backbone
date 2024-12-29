using BiddingApp.Domain.Models.Entities;

namespace BiddingApp.Infrastructure.Dtos.UserDtos
{
    public class AuthenticateResponse
    {
        public int Result { get; set; }
        public User User { get; set; }
    }
}
