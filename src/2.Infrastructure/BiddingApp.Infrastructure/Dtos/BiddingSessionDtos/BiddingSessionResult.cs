using BiddingApp.Domain.Models.Entities;

namespace BiddingApp.Infrastructure.Dtos.BiddingSessionDtos
{
    public class BiddingSessionResult
    {
        public List<BiddingSession> BiddingSessions { get; set; }
        public int TotalItems { get; set; }
        public int ItemCounts { get; set; }
    }
}
