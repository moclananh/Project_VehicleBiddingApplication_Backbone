using BiddingApp.Domain.Models.Entities;

namespace BiddingApp.Infrastructure.Dtos.VehicleDtos
{
    public class VehicleResult
    {
        public List<Vehicle> Vehicles { get; set; }
        public int TotalItems { get; set; }
        public int ItemCounts { get; set; }
    }
}
