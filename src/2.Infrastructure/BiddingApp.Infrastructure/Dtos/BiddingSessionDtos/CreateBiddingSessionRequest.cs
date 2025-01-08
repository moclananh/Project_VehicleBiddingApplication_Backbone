namespace BiddingApp.Infrastructure.Dtos.BiddingSessionDtos
{
    public class CreateBiddingSessionRequest
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal OpeningValue { get; set; }
        public decimal MinimumJumpingValue { get; set; } 
        public int VehicleId { get; set; }
    } 
}
