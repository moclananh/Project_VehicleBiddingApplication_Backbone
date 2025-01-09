using BiddingApp.BuildingBlock.Extentions;
using BiddingApp.Domain.Models.Enums;
using BiddingApp.Infrastructure.Pagination;
using System.Text.Json.Serialization;

namespace BiddingApp.Infrastructure.Dtos.VehicleDtos
{
    public class VehicleFilter : PagingRequest
    {
        public string? Name { get; set; }
        [JsonConverter(typeof(StringEnumConverter<Brand>))]
        public Brand? Brand { get; set; }
        public string? VIN { get; set; }
        public string? Color { get; set; }
        [JsonConverter(typeof(StringEnumConverter<VehicleStatus>))]
        public VehicleStatus? Status { get; set; }
    }
}
