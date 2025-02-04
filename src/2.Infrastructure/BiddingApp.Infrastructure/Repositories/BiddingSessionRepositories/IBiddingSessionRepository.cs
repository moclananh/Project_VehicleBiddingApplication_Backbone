﻿using BiddingApp.Domain.Models.Entities;
using BiddingApp.Infrastructure.Dtos.BiddingSessionDtos;

namespace BiddingApp.Infrastructure.Repositories.BiddingSessionRepositories
{
    public interface IBiddingSessionRepository
    {
        Task<BiddingSessionResult> GetAllBiddingSessionsAsync(BiddingSessionFilter request);
        Task<BiddingSessionResult> GetAllBiddingSessionByUserIdAsync(Guid userId, UserBiddingSessionFilter request);
        Task<BiddingSession> GetBiddingSessionByIdAsync(Guid id);
        Task<bool> FetchBiddingAsync(Guid biddingSessionId, decimal currentBiddingValue); // update newest bidding value & count total bidding
        Task<bool> CreateBiddingSessionAsync(CreateBiddingSessionRequest request);
        Task<bool> CloseBiddingSessionAsync(Guid id); //called when end session
        Task<bool> DisableBiddingSessionAsync(Guid id); //admin optional
        Task<List<Bidding>> GetUserBiddingStatus (Guid sessionId, Guid userId);
    }
}
