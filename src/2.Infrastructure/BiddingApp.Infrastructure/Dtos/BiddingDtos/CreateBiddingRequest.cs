namespace BiddingApp.Infrastructure.Dtos.BiddingDtos
{
    public class CreateBiddingRequest
    {
        public decimal UserCurrentBidding { get; set; }
        public Guid UserId { get; set; }
        public Guid BiddingSessionId { get; set; }
    }
}
