using AutoMapper;
using BiddingApp.Application.Services.VehicleSevices;
using BiddingApp.BuildingBlock.Exceptions;
using BiddingApp.BuildingBlock.Utilities;
using BiddingApp.Domain.Models.Entities;
using BiddingApp.Domain.Models.Enums;
using BiddingApp.Infrastructure;
using BiddingApp.Infrastructure.Dtos.VehicleDtos;
using BiddingApp.Infrastructure.Pagination;
using Microsoft.Extensions.Logging;
using Moq;

namespace BiddingApp.ApplicationTest.VehicleService
{
    public class VehicleServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ILogger<BiddingApp.Application.Services.VehicleSevices.VehicleService>> _loggerMock;
        private readonly BiddingApp.Application.Services.VehicleSevices.VehicleService _vehicleService;

        public VehicleServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _loggerMock = new Mock<ILogger<BiddingApp.Application.Services.VehicleSevices.VehicleService>>();
            _vehicleService = new BiddingApp.Application.Services.VehicleSevices.VehicleService(_unitOfWorkMock.Object, _mapperMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task CreateVehicle_ValidRequest_ShouldReturnSuccessResponse()
        {
            // Arrange
            var request = new CreateVehicleRequest
            {
                Name = "Test Vehicle",
                Description = "Test Description",
                VIN = "VIN123456",
                Price = 50000,
                Color = "Red",
                Brands = Brand.BMW
            };

            _unitOfWorkMock
                .Setup(x => x.VehicleRepository.CreateVehicleAsync(It.IsAny<CreateVehicleRequest>()))
                .ReturnsAsync("VIN123456");

            // Act
            var response = await _vehicleService.CreateVehicle(request);

            // Assert
            Assert.True(response.IsSuccess);
            Assert.Equal("VIN123456", response.Data);
            Assert.Equal(200, response.StatusCode);
        }

        [Fact]
        public async Task CreateVehicle_VinExists_ShouldThrowBadRequestException()
        {
            // Arrange
            var request = new CreateVehicleRequest
            {
                Name = "Test Vehicle",
                Description = "Test Description",
                VIN = "VIN123456",
                Price = 50000,
                Color = "Red",
                Brands = Brand.BMW
            };

            _unitOfWorkMock
                .Setup(x => x.VehicleRepository.CreateVehicleAsync(It.IsAny<CreateVehicleRequest>()))
                .ReturnsAsync((string?)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<BadRequestException>(() => _vehicleService.CreateVehicle(request));
            Assert.Equal(SystemConstants.VehicleMessageResponses.VINExisted, exception.Message);
        }

        [Fact]
        public async Task GetAllVehicles_ValidRequest_ShouldReturnPagingResult()
        {
            // Arrange
            var request = new VehicleFilter { PageNumber = 1, PageSize = 10 };
            var vehicles = new List<Vehicle>
            {
                new Vehicle { Id = 1, Name = "Vehicle1", VIN = "VIN123", Price = 10000, Color = "Blue", Brands = Brand.Nissan },
                new Vehicle { Id = 2, Name = "Vehicle2", VIN = "VIN456", Price = 20000, Color = "Red", Brands = Brand.BMW }
            };

            var paginatedResult = new VehicleResult
            {
                Vehicles = vehicles,
                TotalItems = 12,
                ItemCounts = 2
            };

            // Mock the method that returns VehicleResult
            _unitOfWorkMock
                .Setup(uow => uow.VehicleRepository.GetAllVehiclesAsync(It.IsAny<VehicleFilter>()))
                .ReturnsAsync(paginatedResult);  // Return the mocked result here

            // Mock the AutoMapper setup
            _mapperMock
                .Setup(mapper => mapper.Map<List<VehicleVm>>(It.IsAny<List<Vehicle>>()))
                .Returns(new List<VehicleVm>
                {
                    new VehicleVm { Id = 1, Name = "Vehicle1", VIN = "VIN123", Price = 10000, Color = "Blue", Brands = Brand.Nissan.ToString() },
                    new VehicleVm { Id = 2, Name = "Vehicle2", VIN = "VIN456", Price = 20000, Color = "Red", Brands = Brand.BMW.ToString() }
                });

            // Act
            var response = await _vehicleService.GetAllVehicles(request);

            // Assert
            Assert.True(response.IsSuccess);
            Assert.NotNull(response.Data);
            Assert.Equal(2, response.Data.Items.Count);
            Assert.Equal(200, response.StatusCode);
        }


        [Fact]
        public async Task GetVehicleByVin_ValidVin_ShouldReturnVehicle()
        {
            // Arrange
            var vin = "VIN123";
            var vehicle = new Vehicle { Id = 1, Name = "Vehicle1", VIN = vin, Price = 10000, Color = "Blue", Brands = Brand.BMW };

            _unitOfWorkMock
                .Setup(x => x.VehicleRepository.GetVehicleByVINAsync(It.IsAny<string>()))
                .ReturnsAsync(vehicle);

            _mapperMock
                .Setup(x => x.Map<VehicleVm>(It.IsAny<Vehicle>()))
                .Returns(new VehicleVm { Id = 1, Name = "Vehicle1", VIN = vin, Price = 10000, Color = "Blue", Brands = Brand.BMW.ToString() });

            // Act
            var response = await _vehicleService.GetVehicleByVin(vin);

            // Assert
            Assert.True(response.IsSuccess);
            Assert.NotNull(response.Data);
            Assert.Equal(vin, response.Data.VIN);
        }

        [Fact]
        public async Task GetVehicleByVin_InvalidVin_ShouldThrowNotFoundException()
        {
            // Arrange
            var vin = "INVALID_VIN";

            _unitOfWorkMock
                .Setup(x => x.VehicleRepository.GetVehicleByVINAsync(It.IsAny<string>()))
                .ReturnsAsync((Vehicle?)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<NotFoundException>(() => _vehicleService.GetVehicleByVin(vin));
            Assert.Equal($"Vehicle with VIN Code {vin} does not exist!", exception.Message);
        }

        [Fact]
        public async Task UpdateVehicle_ValidRequest_ShouldReturnSuccessResponse()
        {
            // Arrange
            var id = 1;
            var request = new UpdateVehicleRequest { Name = "Updated Vehicle", Price = 60000 };

            _unitOfWorkMock
                .Setup(x => x.VehicleRepository.GetVehicleByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Vehicle { Id = id, Name = "Old Vehicle" });

            _unitOfWorkMock
                .Setup(x => x.VehicleRepository.UpdateVehicleAsync(It.IsAny<int>(), It.IsAny<UpdateVehicleRequest>()))
                .Verifiable();

            // Act
            var response = await _vehicleService.UpdateVehicle(id, request);

            // Assert
            Assert.True(response.IsSuccess);
            Assert.Equal(200, response.StatusCode);
        }

        [Fact]
        public async Task DeleteVehicle_UnavailableVehicle_ShouldReturnSuccessResponse()
        {
            // Arrange
            var id = 1;

            _unitOfWorkMock
                .Setup(x => x.VehicleRepository.GetVehicleByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Vehicle { Id = id, Status = VehicleStatus.UnAvailable });

            _unitOfWorkMock
                .Setup(x => x.VehicleRepository.DeleteVehicleAsync(It.IsAny<int>()))
                .Verifiable();

            // Act
            var response = await _vehicleService.DeleteVehicle(id);

            // Assert
            Assert.True(response.IsSuccess);
            Assert.Equal(200, response.StatusCode);
        }
    }
}

