using BiddingApp.Domain.Models;
using BiddingApp.Infrastructure.Dtos.UserDtos;

namespace BiddingApp.Application.Services.UserServices
{
    public interface IUserService
    {
        Task<ApiResponse<UserVm>> GetUserByid(Guid id);
        Task<ApiResponse<UserReportResult>> GetUserReport(Guid id, UserReportFilter request);
    }
}
