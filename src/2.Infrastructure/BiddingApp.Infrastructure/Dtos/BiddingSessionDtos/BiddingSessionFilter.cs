using BiddingApp.BuildingBlock.Extentions;
using BiddingApp.Domain.Models.Enums;
using BiddingApp.Infrastructure.Pagination;
using System.Text.Json.Serialization;
namespace BiddingApp.Infrastructure.Dtos.BiddingSessionDtos
{
    public class BiddingSessionFilter : PagingRequest
    {
        public bool? IsActive { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string? VIN { get; set; }
        public string? Name { get; set; }
        [JsonConverter(typeof(StringEnumConverter<Brand>))]
        public Brand? Brand { get; set; }

    }
}
