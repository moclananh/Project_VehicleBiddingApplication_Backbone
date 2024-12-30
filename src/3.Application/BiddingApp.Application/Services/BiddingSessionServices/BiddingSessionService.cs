using AutoMapper;
using BiddingApp.BuildingBlock.Exceptions;
using BiddingApp.BuildingBlock.Utilities;
using BiddingApp.Domain.Models;
using BiddingApp.Infrastructure;
using BiddingApp.Infrastructure.Dtos.BiddingSessionDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace BiddingApp.Application.Services.BiddingSessionServices
{
    public class BiddingSessionService : IBiddingSessionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UnitOfWork> _logger;
        public BiddingSessionService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UnitOfWork> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponse<bool>> CreateBiddingSession(CreateBiddingSessionRequest request)
        {
            try
            {
                // Call the repository to create the bidding session
                await _unitOfWork.BiddingSessionRepository.CreateBiddingSessionAsync(request);

                return new ApiResponse<bool>
                {
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status201Created,
                    Message = SystemConstants.BiddingSessionMessageResponses.BiddingSessionCreated
                };
            }
            catch (Exception ex)
            {
                // Log exception or handle as necessary
                throw new InternalServerException(SystemConstants.InternalMessageResponses.InternalMessageError, ex.Message);
            }
        }

        public async Task<ApiResponse<bool>> DisableBiddingSession(Guid id)
        {
            try
            {
                await _unitOfWork.BiddingSessionRepository.DisableBiddingSessionAsync(id);

                return new ApiResponse<bool>
                {
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status200OK,
                    Message = SystemConstants.BiddingSessionMessageResponses.BiddingSessionUpdated
                };
            }
            catch (Exception ex)
            {
                throw new InternalServerException(SystemConstants.InternalMessageResponses.InternalMessageError, ex.Message);
            }
        }

        public async Task<ApiResponse<bool>> CloseBiddingSession(Guid id)
        {
            try
            {
                await _unitOfWork.BiddingSessionRepository.CloseBiddingSessionAsync(id);

                return new ApiResponse<bool>
                {
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status200OK,
                    Message = SystemConstants.BiddingSessionMessageResponses.BiddingSessionUpdated
                };
            }
            catch (Exception ex)
            {
                throw new InternalServerException(SystemConstants.InternalMessageResponses.InternalMessageError, ex.Message);
            }
        }

        public async Task<ApiResponse<BiddingSessionVm>> GetBiddingSessionById(Guid id)
        {
            try
            {
                var biddingSession = await _unitOfWork.BiddingSessionRepository.GetBiddingSessionByIdAsync(id);

                if (biddingSession is null) throw new NotFoundException("Session not found");

                var biddingSessionVm = _mapper.Map<BiddingSessionVm>(biddingSession);

                return new ApiResponse<BiddingSessionVm>
                {
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Session fetch successfully",
                    Data = biddingSessionVm
                };
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InternalServerException(SystemConstants.InternalMessageResponses.InternalMessageError, ex.Message);
            }
        }

        public Task<ApiResponse<BiddingSessionResult>> GetAllBiddingSessions(BiddingSessionFilter request)
        {
            throw new NotImplementedException();
        }
    }
}
