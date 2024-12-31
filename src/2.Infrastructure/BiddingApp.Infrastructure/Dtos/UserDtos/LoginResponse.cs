
using BiddingApp.Domain.Models;

namespace BiddingApp.Infrastructure.Dtos.UserDtos
{
    public class LoginResponse : ApiResponse<string>
    {
       public UserVm Users { get; set; }
    }
}
