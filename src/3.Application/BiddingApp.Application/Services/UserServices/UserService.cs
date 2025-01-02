using AutoMapper;
using BiddingApp.BuildingBlock.Exceptions;
using BiddingApp.BuildingBlock.Utilities;
using BiddingApp.Domain.Models;
using BiddingApp.Infrastructure;
using BiddingApp.Infrastructure.Dtos.UserDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BiddingApp.Application.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UserService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponse<UserVm>> GetUserByid(Guid id)
        {
            // Fetch user from the repository
            var user = await _unitOfWork.UserRepository.GetUserByIdAsync(id);

            // If user is not found, throw an exception
            if (user == null)
            {
                _logger.LogCritical(SystemConstants.AuthenticateResponses.UserNotExist);
                throw new NotFoundException(SystemConstants.AuthenticateResponses.UserNotExist);
            }
            var userVm = _mapper.Map<UserVm>(user);

            return new ApiResponse<UserVm>
            {
                IsSuccess = true,
                Message = SystemConstants.CommonResponse.FetchSuccess,
                StatusCode = StatusCodes.Status200OK,
                Data = userVm
            };
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
                    Message = SystemConstants.CommonResponse.NoData,
                    StatusCode = StatusCodes.Status404NotFound,
                };
            }

            return new ApiResponse<UserReportResult>
            {
                IsSuccess = true,
                Message = SystemConstants.CommonResponse.FetchSuccess,
                StatusCode = StatusCodes.Status200OK,
                Data = result
            };
        }
    }
}
