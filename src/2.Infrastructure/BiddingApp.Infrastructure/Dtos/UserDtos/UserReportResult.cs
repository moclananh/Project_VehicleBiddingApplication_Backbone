namespace BiddingApp.Infrastructure.Dtos.UserDtos
{
    public class UserReportResult
    {
        public List<UserReportVm> Reports { get; set; }
        public int TotalItems { get; set; }
        public int ItemCounts { get; set; }
    }
}
