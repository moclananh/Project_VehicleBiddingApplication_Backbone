using AutoMapper;
using BiddingApp.BuildingBlock.Exceptions;
using BiddingApp.BuildingBlock.Utilities;
using BiddingApp.Domain.Models;
using BiddingApp.Domain.Models.Enums;
using BiddingApp.Infrastructure;
using BiddingApp.Infrastructure.Dtos.VehicleDtos;
using BiddingApp.Infrastructure.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace BiddingApp.Application.Services.VehicleSevices
{
    public class VehicleService : IVehicleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<VehicleService> _logger;
        public VehicleService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<VehicleService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponse<string>> CreateVehicle(CreateVehicleRequest request)
        {
            try
            {
                var vin = await _unitOfWork.VehicleRepository.CreateVehicleAsync(request);

                if (vin is null) throw new BadRequestException(SystemConstants.VehicleMessageResponses.VINExisted);

                return new ApiResponse<string>
                {
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status200OK,
                    Message = SystemConstants.CommonResponse.CreateSuccess,
                    Data = vin
                };
            }
            catch (BadRequestException)
            {
                _logger.LogError(SystemConstants.VehicleMessageResponses.VINExisted);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, SystemConstants.InternalMessageResponses.InternalMessageError);
                throw new InternalServerException(SystemConstants.InternalMessageResponses.InternalMessageError, ex.Message);
            }
        }

        public async Task<ApiResponse<PagingResult<VehicleVm>>> GetAllVehicles(VehicleFilter request)
        {
            try
            {
                var result = await _unitOfWork.VehicleRepository.GetAllVehiclesAsync(request);

                // Map the Todo entities to TodoVm ViewModels
                var resultVmList = _mapper.Map<List<VehicleVm>>(result.Vehicles);

                // Create the paging result
                var pagingResult = new PagingResult<VehicleVm>(request.PageNumber, request.PageSize, result.TotalItems, result.ItemCounts, resultVmList);

                return new ApiResponse<PagingResult<VehicleVm>>
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

        public async Task<ApiResponse<VehicleVm>> GetVehicleByVin(string vin)
        {
            try
            {
                var vehicle = await _unitOfWork.VehicleRepository.GetVehicleByVINAsync(vin);
                if (vehicle == null) throw new NotFoundException($"Vehicle with VIN Code {vin} does not exist!");
                var vehicleVm = _mapper.Map<VehicleVm>(vehicle);
                return new ApiResponse<VehicleVm>
                {
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status200OK,
                    Message = SystemConstants.CommonResponse.FetchSuccess,
                    Data = vehicleVm
                };
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, SystemConstants.InternalMessageResponses.InternalMessageError);
                throw new InternalServerException(SystemConstants.InternalMessageResponses.InternalMessageError, ex.Message);
            }
        }

        public async Task<ApiResponse<VehicleVm>> GetVehicleById(int id)
        {
            try
            {
                var vehicle = await _unitOfWork.VehicleRepository.GetVehicleByIdAsync(id);
                if (vehicle == null) throw new NotFoundException($"Vehicle with Id {id} does not exist!");
                var vehicleVm = _mapper.Map<VehicleVm>(vehicle);
                return new ApiResponse<VehicleVm>
                {
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status200OK,
                    Message = SystemConstants.CommonResponse.FetchSuccess,
                    Data = vehicleVm
                };
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, SystemConstants.InternalMessageResponses.InternalMessageError);
                throw new InternalServerException(SystemConstants.InternalMessageResponses.InternalMessageError, ex.Message);
            }
        }

        public async Task<ApiResponse<bool>> UpdateVehicle(int id, UpdateVehicleRequest request)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var vehicle = await _unitOfWork.VehicleRepository.GetVehicleByIdAsync(id);
                if (vehicle is null) throw new NotFoundException(SystemConstants.VehicleMessageResponses.VehicleNotFound);
                await _unitOfWork.VehicleRepository.UpdateVehicleAsync(id, request);
                await _unitOfWork.CommitTransactionAsync();
                return new ApiResponse<bool>
                {
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status200OK,
                    Message = SystemConstants.CommonResponse.UpdateSuccess
                };
            }
            catch (BadRequestException)
            {
                throw;
            }
            catch (NotFoundException)
            {
                _logger.LogError(SystemConstants.VehicleMessageResponses.VehicleNotFound);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, SystemConstants.InternalMessageResponses.InternalMessageError);
                throw new InternalServerException(SystemConstants.InternalMessageResponses.InternalMessageError, ex.Message);
            }
        }

        public async Task<ApiResponse<bool>> DeleteVehicle(int id)
        {
            try
            {
                // check vehicle valid
                var vehicle = await _unitOfWork.VehicleRepository.GetVehicleByIdAsync(id);
                if (vehicle is null) throw new NotFoundException(SystemConstants.VehicleMessageResponses.VehicleNotFound);
                if (vehicle.Status != VehicleStatus.UnAvailable) throw new BadRequestException(SystemConstants.VehicleMessageResponses.VehicleGuard);
                
                await _unitOfWork.BeginTransactionAsync();
                await _unitOfWork.VehicleRepository.DeleteVehicleAsync(id);
                await _unitOfWork.CommitTransactionAsync();
                return new ApiResponse<bool>
                {
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status200OK,
                    Message = SystemConstants.CommonResponse.DeleteSuccess
                };
            }
            catch (BadRequestException)
            {
                _logger.LogError(SystemConstants.VehicleMessageResponses.VehicleGuard);
                throw;
            }
            catch (NotFoundException)
            {
                _logger.LogError(SystemConstants.VehicleMessageResponses.VehicleNotFound);
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
