using BiddingApp.Domain.Models;
using BiddingApp.Infrastructure.Dtos.UserDtos;

namespace BiddingApp.Application.Services.AuthenticateServices
{
    public interface IAuthenticateService
    {
        Task<ApiResponse<LoginResponse>> Authencate(LoginVm request);
        Task<ApiResponse<bool>> Register(RegisterVm request);
    }
}
