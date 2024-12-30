using AutoMapper;
using BiddingApp.BuildingBlock.Exceptions;
using BiddingApp.Domain.Models;
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
        private readonly ILogger<UnitOfWork> _logger;
        public VehicleService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UnitOfWork> logger)
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

                if (vin is null) throw new BadRequestException("VIN Number already eixisted");

                return new ApiResponse<string>
                {
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Vehicle created successfully",
                    Data = vin
                };
            }
            catch (BadRequestException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new BadRequestException("Create failed.", ex.Message);
            }
        }

        public async Task<ApiResponse<PagingResult<VehicleVm>>> GetAllVehicles(VehicleFilter request)
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
                Message = "Vehicle fetched successfully",
                Data = pagingResult
            };
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
                    Message = "Vehicle fetched successfully",
                    Data = vehicleVm
                };
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new InternalServerException("Error from server");
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
                    Message = "Vehicle fetched successfully",
                    Data = vehicleVm
                };
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new InternalServerException("Error from server");
            }
        }

        public async Task<ApiResponse<bool>> UpdateVehicle(int id, UpdateVehicleRequest request)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var vehicle = await _unitOfWork.VehicleRepository.GetVehicleByIdAsync(id);
                if (vehicle is null) throw new NotFoundException("Vehicle not found");
                await _unitOfWork.VehicleRepository.UpdateVehicleAsync(id, request);
                await _unitOfWork.CommitTransactionAsync();
                return new ApiResponse<bool>
                {
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Vehicle updated successfully"
                };
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new InternalServerException("Error from server");
            }
        }

        public Task<ApiResponse<bool>> DeleteVehicle(int id)
        {
            throw new NotImplementedException();
        }
    }
}
