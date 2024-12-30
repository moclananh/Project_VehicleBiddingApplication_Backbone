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
                Message = "Bidding sessions fetched successfully",
                Data = pagingResult
            };
        }

        public Task<ApiResponse<VehicleVm>> GetVehicleByVin(string vin)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<bool>> UpdateVehicle(int id, UpdateVehicleRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<bool>> DeleteVehicle(int id)
        {
            throw new NotImplementedException();
        }
    }
}
