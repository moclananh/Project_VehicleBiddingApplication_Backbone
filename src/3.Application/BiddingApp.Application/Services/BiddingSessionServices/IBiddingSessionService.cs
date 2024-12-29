using BiddingApp.Domain.Models;
using BiddingApp.Infrastructure.Dtos.BiddingSessionDtos;

namespace BiddingApp.Application.Services.BiddingSessionServices
{
    public interface IBiddingSessionService
    {
        Task<ApiResponse<bool>> CreateBiddingSession(CreateBiddingSessionRequest request);
        Task<ApiResponse<bool>> CloseBiddingSession(Guid id); //called when end session
        Task<ApiResponse<bool>> DisableBiddingSession(Guid id); //admin optional
        Task<ApiResponse<BiddingSessionVm>> GetBiddingSessionById(Guid id);
        Task<ApiResponse<BiddingSessionResult>> GetAllBiddingSessions(BiddingSessionFilter request);
    }
}

