using BiddingApp.Infrastructure.Pagination;
namespace BiddingApp.Infrastructure.Dtos.BiddingSessionDtos
{
    public class BiddingSessionFilter : PagingRequest
    {
        public bool? IsActive { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string? VIN { get; set; }
    }
}
