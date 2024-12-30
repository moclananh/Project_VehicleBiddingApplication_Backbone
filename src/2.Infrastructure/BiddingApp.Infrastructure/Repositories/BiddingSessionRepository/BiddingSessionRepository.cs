using BiddingApp.BuildingBlock.Exceptions;
using BiddingApp.Domain.Models.EF;
using BiddingApp.Domain.Models.Entities;
using BiddingApp.Domain.Models.Enums;
using BiddingApp.Infrastructure.Dtos.BiddingSessionDtos;
using BiddingApp.Infrastructure.Dtos.VehicleDtos;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BiddingApp.Infrastructure.Repositories.BiddingSessionRepository
{
    public class BiddingSessionRepository : IBiddingSessionRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public BiddingSessionRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CreateBiddingSessionAsync(CreateBiddingSessionRequest request)
        {
            try
            {
                // Define the parameters for the stored procedure
                var parameters = new[]
                {
                    new SqlParameter("@StartTime", request.StartTime),
                    new SqlParameter("@EndTime", request.EndTime),
                    new SqlParameter("@OpeningValue", request.OpeningValue),
                    new SqlParameter("@MinimumJumpingValue", request.MinimumJumpingValue),
                    new SqlParameter("@VehicleId", request.VehicleId)
                };

                // Execute the stored procedure with parameters
                await _dbContext.Database.ExecuteSqlRawAsync(
                    "EXEC dbo.CreateBiddingSession @StartTime, @EndTime, @OpeningValue, @MinimumJumpingValue, @VehicleId;",
                    parameters);

                return true;
            }
            catch (Exception ex)
            {
                throw new BadRequestException("Error creating a new bidding session.", ex.Message);
            }
        }

        public async Task<bool> FetchBiddingAsync(Guid biddingSessionId, decimal currentBiddingValue)
        {
            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@SessionId", biddingSessionId),
                    new SqlParameter("@CurrentBiddingValue", currentBiddingValue)
                };

                // Execute the stored procedure to update the highest bidding value
                await _dbContext.Database.ExecuteSqlRawAsync(
                   "EXEC dbo.FetchBiddingValue @SessionId, @CurrentBiddingValue;", parameters);

                // If the result is greater than 0, the update was successful
                return true;
            }
            catch (Exception ex)
            {
                throw new BadRequestException("Error when updating highest bidding value.", ex.Message);
            }
        }

        public Task<BiddingSessionResult> GetAllBiddingSessionsAsync(BiddingSessionFilter request)
        {
            throw new NotImplementedException();
        }

        public async Task<BiddingSession> GetBiddingSessionByIdAsync(Guid id)
        {
            try
            {
                var biddingSessionResult = await _dbContext.BiddingSessions
                    .FromSqlRaw("EXEC dbo.GetBiddingSessionById @Id = {0}", id)
                    .ToListAsync();

                var biddingSession = biddingSessionResult.FirstOrDefault();

                if (biddingSession != null)
                {
                    var vehicleDetails = await _dbContext.Vehicles
                        .FromSqlRaw("EXEC dbo.GetVehicleById @Id = {0}", biddingSession.VehicleId)
                        .ToListAsync();
                    biddingSession.Vehicle = vehicleDetails.FirstOrDefault();
                }

                return biddingSession;

            }
            catch (Exception)
            {
                throw new InternalServerException("Error when called store procedure");
            }
        }

        public async Task<bool> DisableBiddingSessionAsync(Guid id)
        {
            try
            {
                var parameters = new SqlParameter("@Id", id);
                if (parameters == null) throw new InternalServerException("Id not valid");

                var result = await _dbContext.Database.ExecuteSqlRawAsync(
                    "EXEC dbo.DisableBiddingSession @Id;", parameters);

                return true;
            }
            catch (Exception ex)
            {
                throw new BadRequestException("Error when closing bidding session.", ex.Message);
            }
        }

        public async Task<bool> CloseBiddingSessionAsync(Guid id)
        {
            try
            {
                var parameters = new SqlParameter("@Id", id);
                if (parameters == null) throw new InternalServerException("Id not valid");

                var result = await _dbContext.Database.ExecuteSqlRawAsync(
                    "EXEC dbo.CloseBiddingSession @Id;", parameters);

                return true;
            }
            catch (Exception ex)
            {
                throw new BadRequestException("Error when closing bidding session.", ex.Message);
            }
        }
    }
}
