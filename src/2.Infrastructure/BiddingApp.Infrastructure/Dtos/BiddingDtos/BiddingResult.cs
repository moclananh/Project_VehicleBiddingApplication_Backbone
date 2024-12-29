using BiddingApp.Domain.Models.Entities;

namespace BiddingApp.Infrastructure.Dtos.BiddingDtos
{
    public class BiddingResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public Bidding Biddings { get; set; }
    }
}
