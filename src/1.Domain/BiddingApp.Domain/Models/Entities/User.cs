using BiddingApp.Domain.Models.Enums;

namespace BiddingApp.Domain.Models.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
        public decimal Budget { get; set; }
        public ICollection<Bidding> Bids { get; set; }
    }
}
