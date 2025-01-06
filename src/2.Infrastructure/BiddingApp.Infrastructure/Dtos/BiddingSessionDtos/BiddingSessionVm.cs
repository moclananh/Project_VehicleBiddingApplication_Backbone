using BiddingApp.Infrastructure.Dtos.BiddingDtos;
using BiddingApp.Infrastructure.Dtos.VehicleDtos;

namespace BiddingApp.Infrastructure.Dtos.BiddingSessionDtos
{
    public class BiddingSessionVm
    {
        public Guid Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int TotalBiddingCount { get; set; }
        public decimal HighestBidding { get; set; }
        public decimal MinimumJumpingValue { get; set; }
        public bool IsActive { get; set; }
        public bool IsClosed { get; set; }
        public int VehicleId { get; set; }
        //vehicle information
        public VehicleVm Vehicles { get; set; }
        //user winner
        public ICollection<BiddingVm> Biddings { get; set; }
    }
}
