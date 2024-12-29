using BiddingApp.Infrastructure.Pagination;

namespace BiddingApp.Infrastructure.Dtos.UserDtos
{
    public class UserReportFilter : PagingRequest
    {
        public bool? IsWinner { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public bool? IsClosed { get; set; }
        public string? VehicleName { get; set; }
        public string? VIN { get; set;}
    }
}
