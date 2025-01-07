using BiddingApp.Domain.Models;
using BiddingApp.Infrastructure.Dtos.UserDtos;
using BiddingApp.Infrastructure.Pagination;

namespace BiddingApp.Application.Services.UserServices
{
    public interface IUserService
    {
        Task<ApiResponse<UserVm>> GetUserByid(Guid id);
        Task<ApiResponse<PagingResult<UserReportVm>>> GetUserReport(Guid id, UserReportFilter request);
    }
}
