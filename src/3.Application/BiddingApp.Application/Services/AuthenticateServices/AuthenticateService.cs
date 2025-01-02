using AutoMapper;
using BiddingApp.BuildingBlock.Exceptions;
using BiddingApp.BuildingBlock.Utilities;
using BiddingApp.Domain.Models;
using BiddingApp.Domain.Models.Entities;
using BiddingApp.Infrastructure;
using BiddingApp.Infrastructure.Dtos.UserDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BiddingApp.Application.Services.AuthenticateServices
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly AppSetting _app;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthenticateService> _logger;
        public AuthenticateService(IUnitOfWork unitOfWork, IOptions<AppSetting> appOptions, IMapper mapper, ILogger<AuthenticateService> logger)
        {
            _unitOfWork = unitOfWork;
            _app = appOptions.Value;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<ApiResponse<LoginResponse>> Authencate(LoginVm request)
        {
            var authResponse = await _unitOfWork.AuthenticateRepository.AuthenticateUser(request);

            // Handle user not found case
            if (authResponse.Result == -1 || authResponse.User == null)
            {
                _logger.LogCritical(SystemConstants.AuthenticateResponses.UserNotExist);
                throw new NotFoundException(SystemConstants.AuthenticateResponses.UserNotExist);
            }

            // Password verification using PasswordHasher
            var passwordHasher = new PasswordHasher<LoginVm>();
            var verificationResult = passwordHasher.VerifyHashedPassword(request, authResponse.User.Password, request.Password);

            // If the password verification fails, throw exception
            if (verificationResult == PasswordVerificationResult.Failed)
            {
                _logger.LogCritical(SystemConstants.AuthenticateResponses.IncorrectPassword);
                throw new BadRequestException(SystemConstants.AuthenticateResponses.IncorrectPassword);
            }

            var userVm = _mapper.Map<UserVm>(authResponse.User);
            var token = GenerateToken(authResponse.User);
            // If authentication is successful, return login response
            return new ApiResponse<LoginResponse>
            {
                IsSuccess = true,
                Message = SystemConstants.AuthenticateResponses.UserAuthenticated,
                StatusCode = StatusCodes.Status200OK,
                Data = new LoginResponse
                {
                    Id = userVm.Id,
                    UserName = userVm.UserName,
                    Email = userVm.Email,
                    Budget = userVm.Budget,
                    Role = userVm.Role.ToString(),
                    Token = token
                }
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

        public async Task<ApiResponse<bool>> Register(RegisterVm request)
        {
            // Call the repository via Unit of Work
            var result = await _unitOfWork.AuthenticateRepository.RegisterUser(request);

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
    }
}
