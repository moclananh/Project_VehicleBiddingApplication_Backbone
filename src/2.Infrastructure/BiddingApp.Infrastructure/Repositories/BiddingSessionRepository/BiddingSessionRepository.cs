using BiddingApp.BuildingBlock.Exceptions;
using BiddingApp.BuildingBlock.Utilities;
using BiddingApp.Domain.Models.EF;
using BiddingApp.Domain.Models.Entities;
using BiddingApp.Infrastructure.Dtos.BiddingSessionDtos;
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
                throw new InternalServerException(SystemConstants.InternalMessageResponses.DatabaseBadResponse, ex.Message);
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
                throw new InternalServerException(SystemConstants.InternalMessageResponses.DatabaseBadResponse, ex.Message);
            }
        }

        public async Task<BiddingSessionResult> GetAllBiddingSessionsAsync(BiddingSessionFilter request)
        {
            var totalItemsParam = new SqlParameter("@TotalItem", SqlDbType.Int) { Direction = ParameterDirection.Output };
            var itemCountsParam = new SqlParameter("@ItemCount", SqlDbType.Int) { Direction = ParameterDirection.Output };

            try
            {
                // Execute the stored procedure and get the BiddingSessions
                var biddingSessions = await _dbContext.BiddingSessions
                    .FromSqlRaw(
                        "EXEC dbo.GetBiddingSessionsWithPaging @PageNumber, @PageSize, @IsActive, @StartTime, @EndTime, @VIN, @TotalItem OUTPUT, @ItemCount OUTPUT",
                        new SqlParameter("@PageNumber", request.PageNumber),
                        new SqlParameter("@PageSize", request.PageSize),
                        new SqlParameter("@IsActive", request.IsActive ?? (object)DBNull.Value),
                        new SqlParameter("@StartTime", request.StartTime ?? (object)DBNull.Value),
                        new SqlParameter("@EndTime", request.EndTime ?? (object)DBNull.Value),
                        new SqlParameter("@VIN", request.VIN ?? (object)DBNull.Value),
                        totalItemsParam,
                        itemCountsParam)
                    .ToListAsync();

                // Map the related entities
                if (biddingSessions != null)
                {
                    foreach (var biddingSession in biddingSessions)
                    {
                        var vehicleDetails = await _dbContext.Vehicles
                            .FromSqlRaw("EXEC dbo.GetVehicleById @Id = {0}", biddingSession.VehicleId)
                            .ToListAsync();
                        biddingSession.Vehicle = vehicleDetails.FirstOrDefault();
                    }
                }

                // Retrieve total count
                int totalItems = totalItemsParam.Value != DBNull.Value ? (int)totalItemsParam.Value : 0;
                int itemCounts = itemCountsParam.Value != DBNull.Value ? (int)itemCountsParam.Value : 0;

                // Return both the BiddingSessions and the total count encapsulated in BiddingSessionResult
                return new BiddingSessionResult
                {
                    BiddingSessions = biddingSessions,
                    TotalItems = totalItems,
                    ItemCounts = itemCounts,
                };
            }
            catch (Exception ex)
            {
                throw new InternalServerException(SystemConstants.InternalMessageResponses.DatabaseBadResponse, ex.Message);
            }
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
            catch (Exception ex)
            {
                throw new InternalServerException(SystemConstants.InternalMessageResponses.DatabaseBadResponse, ex.Message);
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
                throw new InternalServerException(SystemConstants.InternalMessageResponses.DatabaseBadResponse, ex.Message);
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
                throw new InternalServerException(SystemConstants.InternalMessageResponses.DatabaseBadResponse, ex.Message);
            }
        }
    }
}
