using BiddingApp.BuildingBlock.Extentions;
using BiddingApp.Domain.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BiddingApp.Infrastructure.Dtos.UserDtos
{
    public class RegisterVm
    {
        public string UserName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        [JsonConverter(typeof(StringEnumConverter<UserRole>))]
        public UserRole Role { get; set; } // Admin, Dealer
        public decimal Budget { get; set; }
    }
}
