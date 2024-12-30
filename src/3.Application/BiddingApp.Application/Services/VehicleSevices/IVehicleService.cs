using BiddingApp.Domain.Models;
using BiddingApp.Infrastructure.Dtos.VehicleDtos;
using BiddingApp.Infrastructure.Pagination;

namespace BiddingApp.Application.Services.VehicleSevices
{
    public interface IVehicleService
    {
        Task<ApiResponse<PagingResult<VehicleVm>>> GetAllVehicles(VehicleFilter request);
        Task<ApiResponse<VehicleVm>> GetVehicleByVin(string vin);
        Task<ApiResponse<string>> CreateVehicle(CreateVehicleRequest request);
        Task<ApiResponse<bool>> UpdateVehicle(int id, UpdateVehicleRequest request);
        Task<ApiResponse<bool>> DeleteVehicle(int id);
    }
}
