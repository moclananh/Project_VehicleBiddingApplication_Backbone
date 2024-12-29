using BiddingApp.BuildingBlock.Exceptions;
using BiddingApp.BuildingBlock.Utilities;
using BiddingApp.Domain.Models.EF;
using BiddingApp.Domain.Models.Entities;
using BiddingApp.Infrastructure.Dtos.UserDtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
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

        public async Task<AuthenticateResponse> AuthenticateUser(LoginVm request)
        {
            var parameters = new[]
              {
                new SqlParameter("@Email", request.Email),
                new SqlParameter("@Password", request.Password),
                new SqlParameter("@UserId", SqlDbType.UniqueIdentifier) { Direction = ParameterDirection.Output },
                new SqlParameter("@UserName", SqlDbType.NVarChar, 256) { Direction = ParameterDirection.Output },
                new SqlParameter("@EmailOut", SqlDbType.NVarChar, 256) { Direction = ParameterDirection.Output },
                new SqlParameter("@HashedPassword", SqlDbType.NVarChar, -1) { Direction = ParameterDirection.Output },
                new SqlParameter("@Role", SqlDbType.Int) { Direction = ParameterDirection.Output },
                new SqlParameter("@Budget", SqlDbType.Decimal) { Direction = ParameterDirection.Output },
                new SqlParameter("@Result", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };
            try
            {
                // Execute the stored procedure
                await _dbContext.Database.ExecuteSqlRawAsync(
                    "EXEC dbo.AuthenticateUser @Email, @Password, @UserId OUTPUT, @UserName OUTPUT, @EmailOut OUTPUT, @HashedPassword OUTPUT, @Role OUTPUT, @Budget OUTPUT, @Result OUTPUT",
                    parameters);

                // Extract the result from output parameters
                var result = (int)parameters.First(p => p.ParameterName == "@Result").Value!;

                // If authentication failed (result code -1), return null user
                if (result == -1)
                {
                    return new AuthenticateResponse
                    {
                        Result = result,
                        User = null
                    };
                }

                // Map the user object from the output parameters
                var user = new User
                {
                    Id = (Guid)parameters.First(p => p.ParameterName == "@UserId").Value!,
                    Username = (string)parameters.First(p => p.ParameterName == "@UserName").Value!,
                    Email = (string)parameters.First(p => p.ParameterName == "@EmailOut").Value!,
                    Role = (Domain.Models.Enums.UserRole)parameters.First(p => p.ParameterName == "@Role").Value!,
                   // Budget = (decimal)parameters.First(p => p.ParameterName == "@Budget").Value!,
                    Password = (string)parameters.First(p => p.ParameterName == "@HashedPassword").Value!,
                };

                // Return the authentication response
                return new AuthenticateResponse
                {
                    Result = result,
                    User = user
                };
            }
            catch (Exception ex)
            {
                throw new BadRequestException("Error in call store procedure, Error: ", ex.Message);
            }
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            try
            {
                var user = await _dbContext.Users
                    .FromSqlRaw("EXEC dbo.GetUserById @Id = {0}", id) // Use parameterized query
                    .ToListAsync();

                return user.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new BadRequestException("Error when calling stored procedure. Error: " + ex.Message);
            }
        }

        public async Task<int> RegisterUser(RegisterVm request)
        {
            // Hash the password before storing it
            var passwordHasher = new PasswordHasher<RegisterVm>();
            var hashedPassword = passwordHasher.HashPassword(request, request.Password);

            // Prepare the parameters for the stored procedure
            var parameters = new[]
            {
                new SqlParameter("@UserName", request.UserName),
                new SqlParameter("@Email", request.Email),
                new SqlParameter("@Password", hashedPassword),
                new SqlParameter("@Role", request.Role),
                new SqlParameter("@Budget", request.Budget),
                new SqlParameter("@Result", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };
            try
            {
                // Execute the stored procedure
                await _dbContext.Database.ExecuteSqlRawAsync(
                    "EXEC dbo.RegisterUser @UserName, @Email, @Password, @Role, @Budget, @Result OUTPUT",
                    parameters);

                // Return the result from output parameters
                return (int)parameters.First(p => p.ParameterName == "@Result").Value!;
            }
            catch (Exception ex)
            {
                throw new BadRequestException("Error in call store procedure, Error: ", ex.Message);
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
                        Id = x.b.Id,
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
                throw new BadRequestException(SystemConstants.InternalMessageResponses.DatabaseBadResponse, ex.Message);
            }
        }
    }
}

