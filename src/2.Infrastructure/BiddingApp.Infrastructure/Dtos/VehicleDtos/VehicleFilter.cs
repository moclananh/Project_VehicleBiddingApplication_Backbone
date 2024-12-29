using BiddingApp.Domain.Models.Enums;
using BiddingApp.Infrastructure.Pagination;

namespace BiddingApp.Infrastructure.Dtos.VehicleDtos
{
    public class VehicleFilter : PagingRequest
    {
        public string Name { get; set; }
        public Brand Brands { get; set; }
        public string VIN { get; set; }
        public string Color { get; set; }
        public VehicleStatus Status { get; set; }
    }
}
