using BiddingApp.Domain.Models;
using BiddingApp.Infrastructure.Dtos.BiddingSessionDtos;
using BiddingApp.Infrastructure.Pagination;

namespace BiddingApp.Application.Services.BiddingSessionServices
{
    public interface IBiddingSessionService
    {
        Task<ApiResponse<bool>> CreateBiddingSession(CreateBiddingSessionRequest request);
        Task<ApiResponse<bool>> CloseBiddingSession(Guid id); //called when end session
        Task<ApiResponse<bool>> DisableBiddingSession(Guid id); //admin optional
        Task<ApiResponse<BiddingSessionVm>> GetBiddingSessionById(Guid id);
        Task<ApiResponse<PagingResult<BiddingSessionVm>>> GetAllBiddingSessions(BiddingSessionFilter request);
    }
}

