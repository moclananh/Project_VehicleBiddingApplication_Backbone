using AutoMapper;
using BiddingApp.Domain.Models;
using BiddingApp.Infrastructure;
using BiddingApp.Infrastructure.Dtos.VehicleDtos;
using BiddingApp.Infrastructure.Pagination;
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

        public Task<ApiResponse<bool>> CreateVehicle(CreateVehicleRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<bool>> DeleteVehicle(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<PagingResult<VehicleVm>>> GetAllVehicles(VehicleFilter request)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<VehicleVm>> GetVehicleByVin(string vin)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<bool>> UpdateVehicle(int id, UpdateVehicleRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
