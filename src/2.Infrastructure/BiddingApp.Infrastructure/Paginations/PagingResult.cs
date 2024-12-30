

namespace BiddingApp.Infrastructure.Pagination
{
    public class PagingResult<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int ItemCounts { get; set; }
        public List<T> Items { get; set; }

        public PagingResult(int pageNumber, int pageSize, int totalItems, int itemCounts, List<T> items)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalItems = totalItems;
            ItemCounts = itemCounts;
            Items = items;
        }
    }

}
