using BiddingApp.BuildingBlock.Exceptions;
using BiddingApp.BuildingBlock.Utilities;
using BiddingApp.Domain.Models.EF;
using BiddingApp.Domain.Models.Entities;
using BiddingApp.Infrastructure.Dtos.BiddingDtos;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BiddingApp.Infrastructure.Repositories.BiddingRepositories
{
    public class BiddingRepository : IBiddingRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public BiddingRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CreateBiddingRequestAsync(CreateBiddingRequest request)
        {
            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@UserCurrentBidding", request.UserCurrentBidding),
                    new SqlParameter("@UserId", request.UserId),
                    new SqlParameter("@BiddingSessionId", request.BiddingSessionId)
                };

                await _dbContext.Database.ExecuteSqlRawAsync(
                    "EXEC dbo.CreateBidding @UserCurrentBidding, @UserId, @BiddingSessionId;",
                    parameters);

                return true;
            }
            catch (Exception ex)
            {
                throw new InternalServerException(SystemConstants.InternalMessageResponses.DatabaseBadResponse, ex.Message);
            }
        }

        public async Task<List<Bidding>> GetBiddingListByBiddingSessionIdAsync(Guid sessionId)
        {
            try
            {
                var sessionIdParam = new SqlParameter("@SessionId", sessionId);

                var biddingList = await _dbContext.Biddings
                  .FromSqlRaw("EXEC dbo.[GetBiddingListBySessionId] @SessionId", sessionIdParam)
                  .ToListAsync();
                return biddingList;
            }
            catch (Exception ex)
            {
                throw new InternalServerException(SystemConstants.InternalMessageResponses.DatabaseBadResponse, ex.Message);
            }
        }

        public async Task<bool> UpdateUserStateAsync(Guid sessionId)
        {
            try
            {
                await _dbContext.Database.ExecuteSqlRawAsync("EXEC dbo.UpdateUserState @SessionId = {0};", sessionId);

                // If the result is greater than 0, the update was successful
                return true;
            }
            catch (Exception ex)
            {
                throw new InternalServerException(SystemConstants.InternalMessageResponses.DatabaseBadResponse, ex.Message);
            }
        }
    }
}
