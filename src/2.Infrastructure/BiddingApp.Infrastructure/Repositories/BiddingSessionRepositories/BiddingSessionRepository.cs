﻿using BiddingApp.BuildingBlock.Exceptions;
using BiddingApp.BuildingBlock.Utilities;
using BiddingApp.Domain.Models.EF;
using BiddingApp.Domain.Models.Entities;
using BiddingApp.Infrastructure.Dtos.BiddingSessionDtos;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BiddingApp.Infrastructure.Repositories.BiddingSessionRepositories
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
                        "EXEC dbo.GetBiddingSessionsWithPaging @PageNumber, @PageSize, @IsActive, @StartTime, @EndTime, @VIN, @Name, @Brand, @TotalItem OUTPUT, @ItemCount OUTPUT",
                        new SqlParameter("@PageNumber", request.PageNumber),
                        new SqlParameter("@PageSize", request.PageSize),
                        new SqlParameter("@IsActive", request.IsActive ?? (object)DBNull.Value),
                        new SqlParameter("@StartTime", request.StartTime ?? (object)DBNull.Value),
                        new SqlParameter("@EndTime", request.EndTime ?? (object)DBNull.Value),
                        new SqlParameter("@VIN", request.VIN ?? (object)DBNull.Value),
                        new SqlParameter("@Name", request.Name ?? (object)DBNull.Value),
                        new SqlParameter("@Brand", request.Brand ?? (object)DBNull.Value),
                        totalItemsParam,
                        itemCountsParam)
                    .ToListAsync();

                // Map the related entities
                if (biddingSessions != null)
                {
                    // Map vehicle information details
                    foreach (var biddingSession in biddingSessions)
                    {
                        var vehicleDetails = await _dbContext.Vehicles
                            .FromSqlRaw("EXEC dbo.GetVehicleById @Id = {0}", biddingSession.VehicleId)
                            .ToListAsync();
                        biddingSession.Vehicle = vehicleDetails.FirstOrDefault();
                    }

                    // Map the winner user of each session
                    foreach (var biddingSession in biddingSessions)
                    {
                        var userWinner = await _dbContext.Biddings
                            .FromSqlRaw("EXEC dbo.GetWinnerUser @SessionId = {0}", biddingSession.Id)
                            .ToListAsync();
                        biddingSession.Biddings = userWinner;
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

        public async Task<BiddingSessionResult> GetAllBiddingSessionByUserIdAsync(Guid userId, UserBiddingSessionFilter request)
        {
            var totalItemsParam = new SqlParameter("@TotalItem", SqlDbType.Int) { Direction = ParameterDirection.Output };
            var itemCountsParam = new SqlParameter("@ItemCount", SqlDbType.Int) { Direction = ParameterDirection.Output };

            try
            {
                // Execute the stored procedure and get the BiddingSessions
                var biddingSessions = await _dbContext.BiddingSessions
                    .FromSqlRaw(
                        "EXEC dbo.[GetBiddingSessionsByUserIdWithPaging] @PageNumber, @PageSize, @StartTime, @EndTime, @VIN, @Name, @Brand, @TotalItem OUTPUT, @ItemCount OUTPUT",
                        new SqlParameter("@PageNumber", request.PageNumber),
                        new SqlParameter("@PageSize", request.PageSize),
                        new SqlParameter("@StartTime", request.StartTime ?? (object)DBNull.Value),
                        new SqlParameter("@EndTime", request.EndTime ?? (object)DBNull.Value),
                        new SqlParameter("@VIN", request.VIN ?? (object)DBNull.Value),
                        new SqlParameter("@Name", request.Name ?? (object)DBNull.Value),
                        new SqlParameter("@Brand", request.Brand ?? (object)DBNull.Value),
                        totalItemsParam,
                        itemCountsParam)
                    .ToListAsync();

                // Map the related entities
                if (biddingSessions != null)
                {
                    // Map vehicle information details
                    foreach (var biddingSession in biddingSessions)
                    {
                        var vehicleDetails = await _dbContext.Vehicles
                            .FromSqlRaw("EXEC dbo.GetVehicleById @Id = {0}", biddingSession.VehicleId)
                            .ToListAsync();
                        biddingSession.Vehicle = vehicleDetails.FirstOrDefault();
                    }

                    // check current user bidding status of each session
                    foreach (var biddingSession in biddingSessions)
                    {
                        var userWinner = await GetUserBiddingStatus(biddingSession.Id, userId);
                        biddingSession.Biddings = userWinner;
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
                    //map vehicle details
                    var vehicleDetails = await _dbContext.Vehicles
                        .FromSqlRaw("EXEC dbo.GetVehicleById @Id = {0}", biddingSession.VehicleId)
                        .ToListAsync();
                    biddingSession.Vehicle = vehicleDetails.FirstOrDefault();

                    //get top 10 users bidding of session
                    var top10bids = await getTop10Bids(biddingSession.Id);
                    if (top10bids != null)
                    {
                        foreach (var item in top10bids)
                        {
                            //map user details
                            var user = await _dbContext.Users
                            .FromSqlRaw("EXEC dbo.GetUserById @Id = {0}", item.UserId)
                            .ToListAsync();
                            item.User = user.FirstOrDefault();
                        }
                    }
                    biddingSession.Biddings = top10bids;
                }
                return biddingSession;
            }
            catch (Exception ex)
            {
                throw new InternalServerException(SystemConstants.InternalMessageResponses.DatabaseBadResponse, ex.Message);
            }
        }

        public async Task<List<Bidding>> GetUserBiddingStatus(Guid sessionId, Guid userId)
        {
            try
            {
               var result = await _dbContext.Biddings
                            .FromSqlRaw("EXEC dbo.[CheckUserBiddingStatus] @SessionId = {0}, @UserId = {1}", sessionId, userId)
                            .ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new InternalServerException(SystemConstants.InternalMessageResponses.DatabaseBadResponse, ex.Message);
            }
        }

        private async Task<List<Bidding>> getTop10Bids(Guid biddingId)
        {
            try
            {
                //get top 10 users bidding of session
                var top10bids = await _dbContext.Biddings
                        .FromSqlRaw("EXEC dbo.GetTop10Bidding @SessionId = {0}", biddingId)
                        .ToListAsync();
                return top10bids;

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
