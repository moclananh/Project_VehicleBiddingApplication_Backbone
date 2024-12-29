using BiddingApp.Infrastructure.Dtos.BiddingSessionDtos;

namespace BiddingApp.Infrastructure.Repositories.BiddingSessionRepository
{
    public interface IBiddingSessionRepository
    {
        Task<BiddingSessionResult> GetAllBiddingSessionsAsync(BiddingSessionFilter request);
        Task<BiddingSessionVm> GetBiddingSessionByIdAsync(Guid id); //fix later <dont using vm>
        Task<bool> FetchBiddingAsync(Guid biddingSessionId, decimal currentBiddingValue); // update newest bidding value & count total bidding
        Task<bool> CreateBiddingSessionAsync(CreateBiddingSessionRequest request);
        Task<bool> CloseBiddingSessionAsync(Guid id); //called when end session
        Task<bool> DisableBiddingSessionAsync(Guid id); //admin optional
    }
}
