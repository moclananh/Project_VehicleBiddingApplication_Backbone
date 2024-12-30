namespace BiddingApp.Domain.Models.Entities
{
    public class BiddingSession
    {
        public Guid Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int TotalBiddingCount {  get; set; }
        public decimal HighestBidding { get; set; }
        public decimal MinimumJumpingValue { get; set; }
        public bool IsActive { get; set; }
        public bool IsClosed { get; set; }
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
        public ICollection<Bidding> Biddings { get; set; }
    }
}
