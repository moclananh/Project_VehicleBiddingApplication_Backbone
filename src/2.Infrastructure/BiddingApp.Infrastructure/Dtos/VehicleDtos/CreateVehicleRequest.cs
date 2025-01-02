using BiddingApp.BuildingBlock.Extentions;
using BiddingApp.Domain.Models.Enums;
using System.Text.Json.Serialization;

namespace BiddingApp.Infrastructure.Dtos.VehicleDtos
{
    public class CreateVehicleRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        [JsonConverter(typeof(StringEnumConverter<Brand>))]
        public Brand Brands { get; set; }
        public string VIN { get; set; }
        public decimal Price { get; set; }
        public string Color { get; set; }
        public string ImageUrl { get; set; }
        public int NumberOfChairs { get; set; }
        public int Horsepower { get; set; }
        public decimal MaximumSpeed { get; set; }
        public decimal TrunkCapacity { get; set; }
    }
}
