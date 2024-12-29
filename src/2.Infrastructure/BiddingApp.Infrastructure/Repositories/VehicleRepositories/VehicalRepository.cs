using BiddingApp.Domain.Models.EF;
using BiddingApp.Domain.Models.Entities;
using BiddingApp.Infrastructure.Dtos.VehicleDtos;

namespace BiddingApp.Infrastructure.Repositories.VehicleRepositories
{
    public class VehicalRepository :IVehicleRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public VehicalRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<bool> CreateVehicleAsync(CreateVehicleRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteVehicleAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<VehicleResult> GetAllTodosAsync(VehicleFilter request)
        {
            throw new NotImplementedException();
        }

        public Task<Vehicle> GetVehicleByVINAsync(string vin)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateVehicleAsync(int id, UpdateVehicleRequest todoVm)
        {
            throw new NotImplementedException();
        }
    }
}
