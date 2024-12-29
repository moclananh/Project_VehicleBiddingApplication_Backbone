

namespace BiddingApp.Infrastructure.Pagination
{
    public class PagingRequest
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 12;
    }
}
