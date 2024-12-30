using BiddingApp.Domain.Models.Enums;

namespace BiddingApp.Infrastructure.Dtos.VehicleDtos
{
    public class UpdateVehicleRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desciption { get; set; }
        public Brand Brands { get; set; }
        public decimal Price { get; set; }
        public string Color { get; set; }
        public string ImageUrl { get; set; }
        public VehicleStatus Status { get; set; }
    }
}
