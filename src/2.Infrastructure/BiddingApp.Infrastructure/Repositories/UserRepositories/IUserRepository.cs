
using BiddingApp.Domain.Models.Entities;
using BiddingApp.Infrastructure.Dtos.UserDtos;

namespace BiddingApp.Infrastructure.Repositories.UserRepositories
{
    public interface IUserRepository
    {
        Task<AuthenticateResponse> AuthenticateUser(LoginVm request);
        Task<int> RegisterUser(RegisterVm request);
        Task<User> GetUserByIdAsync (Guid id); //call for check budget
        Task<UserReportResult> GetUserReportAsync(Guid id, UserReportFilter request);
    }
}
