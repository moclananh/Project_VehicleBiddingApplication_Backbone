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

        public async Task<bool> CreateVehicleAsync(CreateVehicleRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteVehicleAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<VehicleResult> GetAllVehiclesAsync(VehicleFilter request)
        {
            throw new NotImplementedException();
        }

        public async Task<Vehicle> GetVehicleByVINAsync(string vin)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateVehicleAsync(int id, UpdateVehicleRequest todoVm)
        {
            throw new NotImplementedException();
        }
    }
}
