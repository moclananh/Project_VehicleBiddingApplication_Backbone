using BiddingApp.Application.SignalRServices;
using BiddingApp.BuildingBlock.Exceptions;
using BiddingApp.BuildingBlock.Utilities;
using BiddingApp.Domain.Models;
using BiddingApp.Infrastructure;
using BiddingApp.Infrastructure.Dtos.BiddingDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace BiddingApp.Application.Services.BiddingServices
{
    public class BiddingService : IBiddingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<BiddingService> _logger;
        private readonly IBiddingNotificationService _notificationService;

        public BiddingService(
            IUnitOfWork unitOfWork,
            ILogger<BiddingService> logger,
            IBiddingNotificationService notificationService)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _notificationService = notificationService;
        }

        public async Task<ApiResponse<bool>> CreateBidding(CreateBiddingRequest request)
        {
            try
            {
                //check bidding session is valid
                var session = await _unitOfWork.BiddingSessionRepository.GetBiddingSessionByIdAsync(request.BiddingSessionId);
                if (session is null) {
                    _logger.LogError(SystemConstants.BiddingSessionMessageResponses.BiddingSessionNotFound);
                    throw new NotFoundException(SystemConstants.BiddingSessionMessageResponses.BiddingSessionNotFound); 
                }
                if (session.IsClosed)
                {
                    return new ApiResponse<bool>
                    {
                        IsSuccess = false,
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = SystemConstants.BiddingSessionMessageResponses.BiddingSessionClosed
                    };
                }

                //check bidding value valid
                if (request.UserCurrentBidding <= session.HighestBidding)
                {
                    return new ApiResponse<bool>
                    {
                        IsSuccess = false,
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = SystemConstants.BiddingMessageResponses.BiddingNotValid
                    };
                }

                //check user budget is available
                var user = await _unitOfWork.UserRepository.GetUserByIdAsync(request.UserId);
                if (user is null)
                {
                    _logger.LogError(SystemConstants.AuthenticateResponses.UserNotExist);
                    throw new NotFoundException(SystemConstants.AuthenticateResponses.UserNotExist);
                }
                if (user.Budget < request.UserCurrentBidding)
                {
                    return new ApiResponse<bool>
                    {
                        IsSuccess = false,
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = SystemConstants.AuthenticateResponses.UserBudgetCheck
                    };
                }

                //check bidding exist
                if (session.TotalBiddingCount > 0)
                {
                    //upadte state
                    var fetchUserState = await _unitOfWork.BidRepository.UpdateUserStateAsync(request.BiddingSessionId);
                    if (!fetchUserState)
                    {
                        _logger.LogError(SystemConstants.CommonResponse.FetchFailed);
                        throw new BadRequestException(SystemConstants.CommonResponse.FetchFailed);
                    }
                }

                //update highest price & count total bidding
                var fetchHighestBiddingValue = await _unitOfWork.BiddingSessionRepository.FetchBiddingAsync(request.BiddingSessionId, request.UserCurrentBidding);
                if (!fetchHighestBiddingValue)
                {
                    _logger.LogError(SystemConstants.CommonResponse.FetchFailed);
                    throw new BadRequestException(SystemConstants.CommonResponse.FetchFailed);
                }    

                // Call the repository to create the bidding session
                await _unitOfWork.BidRepository.CreateBiddingRequestAsync(request);

                // Broadcast the new bid using the notification service
                await _notificationService.NotifyBiddingUpdateAsync(
                    request.BiddingSessionId,
                    request.UserId,
                    request.UserCurrentBidding);

                return new ApiResponse<bool>
                {
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status201Created,
                    Message = SystemConstants.BiddingMessageResponses.BiddingCreated
                };
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch(BadRequestException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, SystemConstants.InternalMessageResponses.InternalMessageError);
                throw new InternalServerException(SystemConstants.InternalMessageResponses.InternalMessageError, ex.Message);
            }
        }
    }
}
