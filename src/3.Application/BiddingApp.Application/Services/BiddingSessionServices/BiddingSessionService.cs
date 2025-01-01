using AutoMapper;
using BiddingApp.BuildingBlock.Exceptions;
using BiddingApp.BuildingBlock.Utilities;
using BiddingApp.Domain.Models;
using BiddingApp.Domain.Models.Enums;
using BiddingApp.Infrastructure;
using BiddingApp.Infrastructure.Dtos.BiddingSessionDtos;
using BiddingApp.Infrastructure.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace BiddingApp.Application.Services.BiddingSessionServices
{
    public class BiddingSessionService : IBiddingSessionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<BiddingSessionService> _logger;
        public BiddingSessionService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<BiddingSessionService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponse<bool>> CreateBiddingSession(CreateBiddingSessionRequest request)
        {
            try
            {
                // Check vehicle is available
                var vehicle = await _unitOfWork.VehicleRepository.GetVehicleByIdAsync(request.VehicleId);
                if (vehicle is null) throw new NotFoundException(SystemConstants.VehicleMessageResponses.VehicleNotFound);
                if (vehicle.Status != VehicleStatus.Available) throw new BadRequestException(SystemConstants.BiddingSessionMessageResponses.BiddingSessionCreateFailed);

                // Call the repository to create the bidding session
                await _unitOfWork.BiddingSessionRepository.CreateBiddingSessionAsync(request);

                // Update new status for vehicle
                await _unitOfWork.VehicleRepository.UpdateVehicleStatusAsync(request.VehicleId, VehicleStatus.InBidding);

                return new ApiResponse<bool>
                {
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status201Created,
                    Message = SystemConstants.CommonResponse.CreateSuccess
                };
            }
            catch (NotFoundException)
            {
                _logger.LogError(SystemConstants.VehicleMessageResponses.VehicleNotFound);
                throw;
            }
            catch (BadRequestException)
            {
                _logger.LogError(SystemConstants.BiddingSessionMessageResponses.BiddingSessionCreateFailed);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, SystemConstants.InternalMessageResponses.InternalMessageError);
                throw new InternalServerException(SystemConstants.InternalMessageResponses.InternalMessageError, ex.Message);
            }
        }


        public async Task<ApiResponse<PagingResult<BiddingSessionVm>>> GetAllBiddingSessions(BiddingSessionFilter request)
        {
            try
            {
                var result = await _unitOfWork.BiddingSessionRepository.GetAllBiddingSessionsAsync(request);

                // Map the Todo entities to TodoVm ViewModels
                var resultVmList = _mapper.Map<List<BiddingSessionVm>>(result.BiddingSessions);

                // Create the paging result
                var pagingResult = new PagingResult<BiddingSessionVm>(request.PageNumber, request.PageSize, result.TotalItems, result.ItemCounts, resultVmList);

                return new ApiResponse<PagingResult<BiddingSessionVm>>
                {
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status200OK,
                    Message = SystemConstants.CommonResponse.FetchSuccess,
                    Data = pagingResult
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, SystemConstants.InternalMessageResponses.InternalMessageError);
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
                    Message = SystemConstants.CommonResponse.UpdateSuccess
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, SystemConstants.InternalMessageResponses.InternalMessageError);
                throw new InternalServerException(SystemConstants.InternalMessageResponses.InternalMessageError, ex.Message);
            }
        }

        public async Task<ApiResponse<bool>> CloseBiddingSession(Guid id)
        {
            try
            {

                var session = await _unitOfWork.BiddingSessionRepository.GetBiddingSessionByIdAsync(id);
                if (session is null) throw new NotFoundException(SystemConstants.BiddingSessionMessageResponses.BiddingSessionNotFound);

                await _unitOfWork.BiddingSessionRepository.CloseBiddingSessionAsync(id);

                //update status of vehicle when close session
                var isBidding = await _unitOfWork.BidRepository.GetBiddingListByBiddingSessionIdAsync(session.Id);
                if (isBidding.Count > 0) await _unitOfWork.VehicleRepository.UpdateVehicleStatusAsync(session.VehicleId, VehicleStatus.Sold);
                else await _unitOfWork.VehicleRepository.UpdateVehicleStatusAsync(session.VehicleId, VehicleStatus.Available);

                return new ApiResponse<bool>
                {
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status200OK,
                    Message = SystemConstants.CommonResponse.UpdateSuccess
                };
            }

            catch (NotFoundException)
            {
                _logger.LogError(SystemConstants.BiddingSessionMessageResponses.BiddingSessionNotFound);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, SystemConstants.InternalMessageResponses.InternalMessageError);
                throw new InternalServerException(SystemConstants.InternalMessageResponses.InternalMessageError, ex.Message);
            }
        }

        public async Task<ApiResponse<BiddingSessionVm>> GetBiddingSessionById(Guid id)
        {
            try
            {
                var biddingSession = await _unitOfWork.BiddingSessionRepository.GetBiddingSessionByIdAsync(id);

                if (biddingSession is null) throw new NotFoundException(SystemConstants.BiddingSessionMessageResponses.BiddingSessionNotFound);

                var biddingSessionVm = _mapper.Map<BiddingSessionVm>(biddingSession);

                return new ApiResponse<BiddingSessionVm>
                {
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status200OK,
                    Message = SystemConstants.CommonResponse.FetchSuccess,
                    Data = biddingSessionVm
                };
            }
            catch (NotFoundException)
            {
                _logger.LogError(SystemConstants.BiddingSessionMessageResponses.BiddingSessionNotFound);
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
