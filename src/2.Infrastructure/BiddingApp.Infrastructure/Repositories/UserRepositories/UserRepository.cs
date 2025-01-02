using BiddingApp.BuildingBlock.Exceptions;
using BiddingApp.BuildingBlock.Utilities;
using BiddingApp.Domain.Models.EF;
using BiddingApp.Domain.Models.Entities;
using BiddingApp.Infrastructure.Dtos.UserDtos;
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
                // Build the query using LINQ with dynamic filtering
                var query = _dbContext.Biddings
                    .Join(_dbContext.BiddingSessions, b => b.BiddingSessionId, bs => bs.Id, (b, bs) => new { b, bs })
                    .Join(_dbContext.Vehicles, bbs => bbs.bs.VehicleId, v => v.Id, (bbs, v) => new { bbs.b, bbs.bs, v })
                    .Where(x => x.b.UserId == id);

                // Apply filters dynamically based on the UserReportFilter
                if (request.IsWinner.HasValue)
                {
                    query = query.Where(x => x.b.IsWinner == request.IsWinner.Value);
                }

                if (request.StartTime.HasValue)
                {
                    query = query.Where(x => x.bs.StartTime >= request.StartTime.Value);
                }

                if (request.EndTime.HasValue)
                {
                    query = query.Where(x => x.bs.EndTime <= request.EndTime.Value);
                }

                if (request.IsClosed.HasValue)
                {
                    query = query.Where(x => x.bs.IsClosed == request.IsClosed.Value);
                }

                if (!string.IsNullOrEmpty(request.VehicleName))
                {
                    query = query.Where(x => x.v.Name.Contains(request.VehicleName));
                }

                if (!string.IsNullOrEmpty(request.VIN))
                {
                    query = query.Where(x => x.v.VIN.Contains(request.VIN));
                }

                // Get the total count of matching records before paging
                var totalCount = await query.CountAsync();

                // Apply paging
                var reports = await query
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .Select(x => new UserReportVm
                    {
                        BiddingId = x.b.Id,
                        UserCurrentBiddingValue = x.b.UserCurrentBidding,
                        IsWinner = x.b.IsWinner,
                        SessionId = x.bs.Id,
                        StartTime = x.bs.StartTime,
                        EndTime = x.bs.EndTime,
                        IsClosed = x.bs.IsClosed,
                        VehicleName = x.v.Name,
                        VIN = x.v.VIN,
                        ImageUrl = x.v.ImageUrl
                    })
                    .ToListAsync();

                // Return paginated result
                return new UserReportResult
                {
                    Reports = reports,
                    TotalCount = totalCount
                };
            }
            catch (Exception ex)
            {
                throw new InternalServerException(SystemConstants.InternalMessageResponses.DatabaseBadResponse, ex.Message);
            }
        }
    }
}

