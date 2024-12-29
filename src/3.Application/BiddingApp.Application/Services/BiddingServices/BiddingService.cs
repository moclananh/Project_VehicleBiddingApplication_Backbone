using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly ILogger<UnitOfWork> _logger;
        public BiddingService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UnitOfWork> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponse<bool>> CreateBidding(CreateBiddingRequest request)
        {
            try
            {
                //check bidding session is valid
                var session = await _unitOfWork.BiddingSessionRepository.GetBiddingSessionByIdAsync(request.BiddingSessionId);
                if (session is null) throw new NotFoundException("Bidding session not found");
                if (session.IsClosed)
                {
                    return new ApiResponse<bool>
                    {
                        IsSuccess = false,
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "Bidding session was closed"
                    };
                }

                //check bidding value valid
                if (request.UserCurrentBidding <= session.HighestBidding)
                {
                    return new ApiResponse<bool>
                    {
                        IsSuccess = false,
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "Bidding value must be greater than current value"
                    };
                }

                //check user budget is available
                var user = await _unitOfWork.UserRepository.GetUserByIdAsync(request.UserId);
                if (user is null) throw new NotFoundException(SystemConstants.AuthenticateResponses.UserChecked);
                if (user.Budget < request.UserCurrentBidding)
                {
                    return new ApiResponse<bool>
                    {
                        IsSuccess = false,
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "Budget not available to bid"
                    };
                }

                //check bidding exist
                var checkBiddingExist = await _unitOfWork.BidRepository.GetBiddingListByBiddingSessionIdAsync(request.BiddingSessionId);
                if (checkBiddingExist.Count > 0)
                {
                    //upadte state
                    var fetchUserState = await _unitOfWork.BidRepository.UpdateUserStateAsync(request.BiddingSessionId);
                    if (!fetchUserState) throw new BadRequestException("Fetch state failed");
                }

                //update highest price & count total bidding
                var fetchHighestBiddingValue = await _unitOfWork.BiddingSessionRepository.FetchBiddingAsync(request.BiddingSessionId, request.UserCurrentBidding);
                if (!fetchHighestBiddingValue) throw new BadRequestException("Fetch highest bidding value failied");

                // Call the repository to create the bidding session
                await _unitOfWork.BidRepository.CreateBiddingRequestAsync(request);


                return new ApiResponse<bool>
                {
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status201Created,
                    Message = SystemConstants.BiddingMessageResponses.BiddingCreated
                };
            }
            catch (Exception ex)
            {
                // Log exception or handle as necessary
                throw new InternalServerException(SystemConstants.InternalMessageResponses.InternalMessageError, ex.Message);
            }
        }
    }
}
