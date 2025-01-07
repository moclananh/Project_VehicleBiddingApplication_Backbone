using BiddingApp.BuildingBlock.Exceptions;
using BiddingApp.BuildingBlock.Utilities;
using BiddingApp.Domain.Models.EF;
using BiddingApp.Domain.Models.Entities;
using BiddingApp.Infrastructure.Dtos.UserDtos;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BiddingApp.Infrastructure.Repositories.UserRepositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
      
        public async Task<User> GetUserByIdAsync(Guid id)
        {
            try
            {
                var user = await _dbContext.Users
                    .FromSqlRaw("EXEC dbo.GetUserById @Id = {0}", id)
                    .ToListAsync();

                return user.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new InternalServerException(SystemConstants.InternalMessageResponses.DatabaseBadResponse, ex.Message);
            }
        }

        public async Task<UserReportResult> GetUserReportAsync(Guid id, UserReportFilter request)
        {
            try
            {
                // Prepare the connection and command
                var connection = _dbContext.Database.GetDbConnection();

                // Open connection
                await connection.OpenAsync();

                // Prepare stored procedure parameters
                var parameters = new DynamicParameters();
                parameters.Add("UserId", id, DbType.Guid);
                parameters.Add("PageNumber", request.PageNumber, DbType.Int32);
                parameters.Add("PageSize", request.PageSize, DbType.Int32);
                parameters.Add("IsWinner", request.IsWinner, DbType.Boolean);
                parameters.Add("StartTime", request.StartTime, DbType.DateTime);
                parameters.Add("EndTime", request.EndTime, DbType.DateTime);
                parameters.Add("IsClosed", request.IsClosed, DbType.Boolean);
                parameters.Add("VIN", request.VIN, DbType.String);
                parameters.Add("VehicleName", request.VehicleName, DbType.String);
                parameters.Add("TotalItems", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("ItemCount", dbType: DbType.Int32, direction: ParameterDirection.Output);

                // Execute the stored procedure and map the results to the ViewModel
                var reports = await connection.QueryAsync<UserReportVm>(
                    "dbo.GetUserReportByUserIdWithPaging",
                    parameters,
                    commandType: CommandType.StoredProcedure);

                var totalItems = parameters.Get<int>("TotalItems");
                var itemCounts = parameters.Get<int>("ItemCount");

                // Return the result
                return new UserReportResult
                {
                    Reports = reports.ToList(),
                    TotalItems = totalItems,
                    ItemCounts = itemCounts
                };
            }
            catch (Exception ex)
            {
                throw new InternalServerException(SystemConstants.InternalMessageResponses.DatabaseBadResponse, ex.Message);
            }
        }
    }
}

