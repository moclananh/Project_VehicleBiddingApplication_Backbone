using BiddingApp.BuildingBlock.Exceptions;
using BiddingApp.Domain.Models.EF;
using BiddingApp.Domain.Models.Entities;
using BiddingApp.Domain.Models.Enums;
using BiddingApp.Infrastructure.Dtos.VehicleDtos;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BiddingApp.Infrastructure.Repositories.VehicleRepositories
{
    public class VehicalRepository : IVehicleRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public VehicalRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> CreateVehicleAsync(CreateVehicleRequest request)
        {
            var vinOutParam = new SqlParameter
            {
                ParameterName = "@Result",
                SqlDbType = SqlDbType.NVarChar,
                Size = 50,
                Direction = ParameterDirection.Output
            };

            try
            {
                // Execute the stored procedure
                await _dbContext.Database.ExecuteSqlRawAsync(
                    "EXEC dbo.CreateVehicle @Name, @Description, @Brand, @VIN, @Price, @Color, @ImageUrl, @Result OUTPUT",
                    new SqlParameter("@Name", request.Name),
                    new SqlParameter("@Description", request.Desciption),
                    new SqlParameter("@Brand", request.Brands),
                    new SqlParameter("@VIN", request.VIN),
                    new SqlParameter("@Price", request.Price),
                    new SqlParameter("@Color", request.Color),
                    new SqlParameter("@ImageUrl", request.ImageUrl),
                    vinOutParam
                );

                var result = vinOutParam.Value as string;
                return result;
            }
            catch (Exception ex)
            {
                throw new InternalServerException("Error when calling the CreateVehicle stored procedure", ex.Message);
            }
        }

        public async Task<VehicleResult> GetAllVehiclesAsync(VehicleFilter request)
        {
            var totalItemsParam = new SqlParameter("@TotalItem", SqlDbType.Int) { Direction = ParameterDirection.Output };
            var itemCountsParam = new SqlParameter("@ItemCount", SqlDbType.Int) { Direction = ParameterDirection.Output };
            try
            {
                // Execute the stored procedure and fetch the vehicles
                var vehicles = await _dbContext.Vehicles
                    .FromSqlRaw(
                        "EXEC dbo.GetVehiclesWithPaging @PageNumber, @PageSize, @Name, @Brand, @VIN, @Color, @Status, @TotalItem OUTPUT, @ItemCount OUTPUT",
                        new SqlParameter("@PageNumber", request.PageNumber),
                        new SqlParameter("@PageSize", request.PageSize),
                        new SqlParameter("@Name", request.Name ?? (object)DBNull.Value),
                        new SqlParameter("@Brand", request.Brands ?? (object)DBNull.Value),
                        new SqlParameter("@VIN", request.VIN ?? (object)DBNull.Value),
                        new SqlParameter("@Color", request.Color ?? (object)DBNull.Value),
                        new SqlParameter("@Status", request.Status ?? (object)DBNull.Value),
                        totalItemsParam,
                        itemCountsParam
                    )
                    .ToListAsync();

                // Retrieve total count
                int totalItems = totalItemsParam.Value != DBNull.Value ? (int)totalItemsParam.Value : 0;
                int itemCounts = itemCountsParam.Value != DBNull.Value ? (int)itemCountsParam.Value : 0;

                // Return the result
                return new VehicleResult
                {
                    Vehicles = vehicles,
                    TotalItems = totalItems,
                    ItemCounts = itemCounts
                };
            }
            catch (Exception ex)
            {
                throw new InternalServerException("Error when calling the GetVehiclesWithPaging stored procedure", ex.Message);
            }
        }

        public async Task<Vehicle> GetVehicleByVINAsync(string vin)
        {
            try
            {
                // Execute the stored procedure and fetch the vehicle
                var vehicle = await _dbContext.Vehicles
                    .FromSqlRaw("EXEC dbo.GetVehicleByVIN @VIN",
                        new SqlParameter("@VIN", vin))
                    .ToListAsync();
                return vehicle.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new InternalServerException("Error when calling the GetVehicleByVIN stored procedure", ex.Message);
            }
        }

        public async Task<bool> UpdateVehicleAsync(int id, UpdateVehicleRequest request)
        {
            try
            {
                // Execute the stored procedure
                await _dbContext.Database.ExecuteSqlRawAsync(
                "EXEC dbo.UpdateVehicle @Id, @Name, @Description, @Brand, @Price, @Color, @ImageUrl, @Status",
                new SqlParameter("@Id", id),
                new SqlParameter("@Name", request.Name),
                new SqlParameter("@Description", request.Desciption),
                new SqlParameter("@Brand", request.Brands),
                new SqlParameter("@Price", request.Price),
                new SqlParameter("@Color", request.Color),
                new SqlParameter("@ImageUrl", request.ImageUrl),
                new SqlParameter("@Status", request.Status));

                return true;
            }
            catch (Exception ex)
            {
                throw new InternalServerException("Error when calling the UpdateVehicle stored procedure", ex.Message);
            }
        }

        public async Task<bool> UpdateVehicleStatusAsync(int id, VehicleStatus status)
        {
            try
            {
                // Execute the stored procedure
                await _dbContext.Database.ExecuteSqlRawAsync(
                    "EXEC dbo.UpdateVehicleStatus @Id, @Status",
                    new SqlParameter("@Id", id),
                    new SqlParameter("@Status", status));

                return true;
            }
            catch (Exception ex)
            {
                throw new InternalServerException("Error when calling the UpdateVehicleStatus stored procedure", ex.Message);
            }
        }

        public async Task<Vehicle> GetVehicleByIdAsync(int id)
        {
            try
            {
                var vehicle = await _dbContext.Vehicles
                    .FromSqlRaw("EXEC dbo.GetVehicleById @Id", new SqlParameter("@Id", id))
                    .ToListAsync();
                return vehicle.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new InternalServerException("Error when calling the GetVehicleByVIN stored procedure", ex.Message);
            }
        }

        public async Task<bool> DeleteVehicleAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
