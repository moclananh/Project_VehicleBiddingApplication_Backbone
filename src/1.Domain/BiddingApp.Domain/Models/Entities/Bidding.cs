namespace BiddingApp.Domain.Models.Entities
{
    public class Bidding
    {
        public int Id { get; set; }
        public decimal UserCurrentBidding { get; set; }
        public bool IsWinner { get; set; }
        public DateTime BiddingAt { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid BiddingSessionId { get; set; }
        public BiddingSession BiddingSession { get; set; }
    }
}
