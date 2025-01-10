using BiddingApp.Domain.Models;
using BiddingApp.Infrastructure.Dtos.BiddingSessionDtos;
using BiddingApp.Infrastructure.Pagination;

namespace BiddingApp.Application.Services.BiddingSessionServices
{
    public interface IBiddingSessionService
    {
        Task<ApiResponse<bool>> CreateBiddingSession(CreateBiddingSessionRequest request);
        Task<ApiResponse<bool>> CloseBiddingSession(Guid id); //called when end session (this is manual option)
        Task<ApiResponse<bool>> DisableBiddingSession(Guid id); //admin optional
        Task<ApiResponse<BiddingSessionVm>> GetBiddingSessionById(Guid id);
        Task<ApiResponse<UserBiddingSessionVm>> GetBiddingSessionByIdWithUserState(Guid userId, Guid sessionId);
        Task<ApiResponse<PagingResult<BiddingSessionVm>>> GetAllBiddingSessions(BiddingSessionFilter request);
        Task<ApiResponse<PagingResult<BiddingSessionVm>>> GetAllBiddingSessionsWithUserState(Guid userId, UserBiddingSessionFilter request);
    }
}

