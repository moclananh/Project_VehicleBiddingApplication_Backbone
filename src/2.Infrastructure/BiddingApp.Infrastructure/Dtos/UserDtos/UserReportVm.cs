namespace BiddingApp.Infrastructure.Dtos.UserDtos
{
    public class UserReportVm
    {
        public int Id { get; set; }
        public decimal UserCurrentBiddingValue { get; set; }
        public bool IsWinner { get; set; }
        public Guid SessionId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsClosed { get; set; }
        public string VehicleName { get; set; }
        public string VIN {  get; set; }
        public string ImageUrl { get; set; }
    }
}
