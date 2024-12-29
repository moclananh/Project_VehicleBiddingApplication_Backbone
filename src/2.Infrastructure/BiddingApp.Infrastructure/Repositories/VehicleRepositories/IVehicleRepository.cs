using BiddingApp.Domain.Models.Entities;
using BiddingApp.Infrastructure.Dtos.VehicleDtos;

namespace BiddingApp.Infrastructure.Repositories.VehicleRepositories
{
    public interface IVehicleRepository
    {
        Task<VehicleResult> GetAllTodosAsync(VehicleFilter request);
        Task<Vehicle> GetVehicleByVINAsync(string vin);
        Task<bool> CreateVehicleAsync(CreateVehicleRequest request);
        Task<bool> UpdateVehicleAsync(int id, UpdateVehicleRequest todoVm);
        Task<bool> DeleteVehicleAsync(int id);
    }
}
