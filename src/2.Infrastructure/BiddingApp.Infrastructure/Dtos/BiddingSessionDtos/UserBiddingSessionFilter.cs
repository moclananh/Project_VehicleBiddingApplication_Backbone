using BiddingApp.Infrastructure.Pagination;

namespace BiddingApp.Infrastructure.Dtos.BiddingSessionDtos
{
    public class UserBiddingSessionFilter : PagingRequest
    {
            public DateTime? StartTime { get; set; }
            public DateTime? EndTime { get; set; }
            public string? VIN { get; set; }
    }
}
