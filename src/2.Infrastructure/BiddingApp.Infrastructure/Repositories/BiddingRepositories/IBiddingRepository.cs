using BiddingApp.Domain.Models.Entities;
using BiddingApp.Infrastructure.Dtos.BiddingDtos;

namespace BiddingApp.Infrastructure.Repositories.BiddingRepositories
{
    public interface IBiddingRepository
    {
        Task<bool> CreateBiddingRequestAsync(CreateBiddingRequest request);
        Task<bool> UpdateUserStateAsync(Guid sessionId); // called for reset userState (isWinner) of all user = false
        Task<List<Bidding>> GetBiddingListByBiddingSessionIdAsync (Guid sessionId);
    }
}
