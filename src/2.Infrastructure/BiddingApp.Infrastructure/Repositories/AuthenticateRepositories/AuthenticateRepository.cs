using BiddingApp.BuildingBlock.Exceptions;
using BiddingApp.BuildingBlock.Utilities;
using BiddingApp.Domain.Models.EF;
using BiddingApp.Domain.Models.Entities;
using BiddingApp.Infrastructure.Dtos.UserDtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BiddingApp.Infrastructure.Repositories.AuthenticateRepositories
{
    public class AuthenticateRepository : IAuthenticateRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AuthenticateRepository(ApplicationDbContext dbContext)
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
                    Budget = (decimal)parameters.First(p => p.ParameterName == "@Budget").Value!,
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
                throw new InternalServerException(SystemConstants.InternalMessageResponses.DatabaseBadResponse, ex.Message);
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
            catch (BadRequestException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InternalServerException(SystemConstants.InternalMessageResponses.DatabaseBadResponse, ex.Message);
            }
        }
    }
}
