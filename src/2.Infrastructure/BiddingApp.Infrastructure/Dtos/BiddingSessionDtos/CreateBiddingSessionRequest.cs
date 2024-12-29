namespace BiddingApp.Infrastructure.Dtos.BiddingSessionDtos
{
    public class CreateBiddingSessionRequest
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal OpeningValue { get; set; }
        public decimal MinimumJumpingValue { get; set; } //FE take care this one (set defaut bidding value = BiddinghighestValue += MinimumJumpingValue)
        public int VehicleId { get; set; }
    } 
}
