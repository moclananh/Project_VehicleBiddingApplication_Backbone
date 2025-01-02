using BiddingApp.Infrastructure.Dtos.UserDtos;

namespace BiddingApp.Infrastructure.Repositories.AuthenticateRepositories
{
    public interface IAuthenticateRepository
    {
        Task<AuthenticateResponse> AuthenticateUser(LoginVm request);
        Task<int> RegisterUser(RegisterVm request);
    }
}
