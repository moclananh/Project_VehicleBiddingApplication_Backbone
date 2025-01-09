using BiddingApp.Infrastructure.Dtos.UserDtos;

namespace BiddingApp.Infrastructure.Dtos.BiddingDtos
{
    public class BiddingVm
    {
        public Guid UserId { get; set; }
        public decimal UserCurrentBidding { get; set; }
        public bool IsWinner { get; set; }
        public Guid BiddingSessionId { get; set; }
        public DateTime? BiddingAt { get; set;}
        public UserVm Users { get; set; }
    }
}
