using BiddingApp.Domain.Models.Entities;
using BiddingApp.Domain.Models.Enums;
using BiddingApp.Infrastructure.Dtos.VehicleDtos;

namespace BiddingApp.Infrastructure.Repositories.VehicleRepositories
{
    public interface IVehicleRepository
    {
        Task<VehicleResult> GetAllVehiclesAsync(VehicleFilter request);
        Task<Vehicle> GetVehicleByVINAsync(string vin);
        Task<Vehicle> GetVehicleByIdAsync(int id);
        Task<string> CreateVehicleAsync(CreateVehicleRequest request);
        Task<bool> UpdateVehicleAsync(int id, UpdateVehicleRequest request);
        Task<bool> UpdateVehicleStatusAsync(int id, VehicleStatus status);
        Task<bool> DeleteVehicleAsync(int id);
    }
} 
