using AutoMapper;
using BiddingApp.BuildingBlock.Exceptions;
using BiddingApp.BuildingBlock.Utilities;
using BiddingApp.Domain.Models;
using BiddingApp.Domain.Models.Entities;
using BiddingApp.Infrastructure;
using BiddingApp.Infrastructure.Dtos.UserDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BiddingApp.Application.Services.UserServices
{
    public class UserService : IUserService
    {

        private readonly AppSetting _app;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IOptions<AppSetting> appOptions, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _app = appOptions.Value;
            _mapper = mapper;
        }

        public async Task<LoginResponse> Authencate(LoginVm request)
        {
            var authResponse = await _unitOfWork.UserRepository.AuthenticateUser(request);

            // Handle user not found case
            if (authResponse.Result == -1 || authResponse.User == null)
            {
                throw new NotFoundException("UserNotFound");
            }

            // Password verification using PasswordHasher
            var passwordHasher = new PasswordHasher<LoginVm>();
            var verificationResult = passwordHasher.VerifyHashedPassword(request, authResponse.User.Password, request.Password);

            // If the password verification fails, throw exception
            if (verificationResult == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException(SystemConstants.AuthenticateResponses.IncorrectPassword);
            }

            // If authentication is successful, return login response
            return new LoginResponse
            {
                IsSuccess = true,
                Message = SystemConstants.AuthenticateResponses.UserAuthenticated,
                StatusCode = StatusCodes.Status200OK,
                Id = authResponse.User.Id,
                UserName = authResponse.User.Username,
                Email = authResponse.User.Email,
                Data = GenerateToken(authResponse.User)
            };
        }

        public async Task<ApiResponse<UserVm>> GetUserByid(Guid id)
        {
            // Fetch user from the repository
            var user = await _unitOfWork.UserRepository.GetUserByIdAsync(id);

            // If user is not found, throw an exception
            if (user == null)
            {
                throw new NotFoundException($"User with ID {id} not found.");
            }
            var userVm = _mapper.Map<UserVm>(user);

            return new ApiResponse<UserVm>
            {
                IsSuccess = true,
                Message = "User retrieved successfully.",
                StatusCode = StatusCodes.Status200OK,
                Data = userVm
            };
        }

        public async Task<ApiResponse<bool>> Register(RegisterVm request)
        {
            // Call the repository via Unit of Work
            var result = await _unitOfWork.UserRepository.RegisterUser(request);

            // Handle result
            return result switch
            {
                -1 => throw new BadRequestException(SystemConstants.AuthenticateResponses.EmailChecked),
                -2 => throw new BadRequestException(SystemConstants.AuthenticateResponses.UsernameChecked),
                1 => new ApiResponse<bool>
                {
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status201Created,
                    Message = SystemConstants.AuthenticateResponses.UserRegistered
                },
                _ => throw new InternalServerException(SystemConstants.InternalMessageResponses.InternalMessageError)
            };
        }

        private string GenerateToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(_app.SecretKey);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim("Id", user.Id.ToString()),
                new Claim("TokenId", Guid.NewGuid().ToString())
            }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(secretKeyBytes),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescription);
            return jwtTokenHandler.WriteToken(token);
        }


        public async Task<ApiResponse<UserReportResult>> GetUserReport(Guid id, UserReportFilter request)
        {
            // Fetch user from the repository
            var result = await _unitOfWork.UserRepository.GetUserReportAsync(id, request);

            // If user is not found, throw an exception
            if (result.Reports.Count <= 0)
            {
                return new ApiResponse<UserReportResult>
                {
                    IsSuccess = true,
                    Message = "No data bidding.",
                    StatusCode = StatusCodes.Status404NotFound,
                };
            }

            return new ApiResponse<UserReportResult>
            {
                IsSuccess = true,
                Message = "User retrieved successfully.",
                StatusCode = StatusCodes.Status200OK,
                Data = result
            };
        }
    }
}
