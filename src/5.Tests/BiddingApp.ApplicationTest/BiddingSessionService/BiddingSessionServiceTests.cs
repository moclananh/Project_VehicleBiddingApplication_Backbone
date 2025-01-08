using AutoMapper;
using BiddingApp.Application.Services.BiddingSessionServices;
using BiddingApp.BuildingBlock.Exceptions;
using BiddingApp.BuildingBlock.Utilities;
using BiddingApp.Domain.Models.Entities;
using BiddingApp.Domain.Models.Enums;
using BiddingApp.Infrastructure;
using BiddingApp.Infrastructure.Dtos.BiddingSessionDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;

namespace BiddingApp.ApplicationTest.BiddingSessionService
{
    public class BiddingSessionServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ILogger<BiddingApp.Application.Services.BiddingSessionServices.BiddingSessionService>> _loggerMock;
        private readonly BiddingApp.Application.Services.BiddingSessionServices.BiddingSessionService _biddingSessionService;

        public BiddingSessionServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _loggerMock = new Mock<ILogger<BiddingApp.Application.Services.BiddingSessionServices.BiddingSessionService>>();
            _biddingSessionService = new BiddingApp.Application.Services.BiddingSessionServices.BiddingSessionService(_unitOfWorkMock.Object, _mapperMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task CreateBiddingSession_ValidRequest_ShouldReturnSuccess()
        {
            // Arrange
            var request = new CreateBiddingSessionRequest
            {
                StartTime = DateTime.Now.AddMinutes(10),
                EndTime = DateTime.Now.AddHours(1),
                OpeningValue = 10000,
                MinimumJumpingValue = 500,
                VehicleId = 1
            };

            var vehicle = new Vehicle
            {
                Id = 1,
                Status = VehicleStatus.Available
            };

            // Mock vehicle fetch and check
            _unitOfWorkMock
                .Setup(uow => uow.VehicleRepository.GetVehicleByIdAsync(request.VehicleId))
                .ReturnsAsync(vehicle);

            // Mock CreateBiddingSessionAsync to simulate stored procedure call
            _unitOfWorkMock
                .Setup(uow => uow.BiddingSessionRepository.CreateBiddingSessionAsync(It.IsAny<CreateBiddingSessionRequest>()))
                .ReturnsAsync(true);  

            // Mock updating vehicle status
            _unitOfWorkMock
                .Setup(uow => uow.VehicleRepository.UpdateVehicleStatusAsync(request.VehicleId, VehicleStatus.InBidding))
                .ReturnsAsync(true); 

            // Act
            var response = await _biddingSessionService.CreateBiddingSession(request);

            // Assert
            Assert.True(response.IsSuccess);
            Assert.Equal(StatusCodes.Status201Created, response.StatusCode);
            Assert.Equal(SystemConstants.CommonResponse.CreateSuccess, response.Message);
        }

        [Fact]
        public async Task CreateBiddingSession_VehicleNotFound_ShouldThrowNotFoundException()
        {
            // Arrange
            var request = new CreateBiddingSessionRequest
            {
                StartTime = DateTime.Now.AddMinutes(10),
                EndTime = DateTime.Now.AddHours(1),
                OpeningValue = 10000,
                MinimumJumpingValue = 500,
                VehicleId = 1
            };

            _unitOfWorkMock
                .Setup(uow => uow.VehicleRepository.GetVehicleByIdAsync(request.VehicleId))
                .ReturnsAsync((Vehicle)null);  // Mock vehicle not found

            // Act & Assert
            var exception = await Assert.ThrowsAsync<NotFoundException>(() => _biddingSessionService.CreateBiddingSession(request));
            Assert.Equal(SystemConstants.VehicleMessageResponses.VehicleNotFound, exception.Message);
        }

        [Fact]
        public async Task CreateBiddingSession_VehicleNotAvailable_ShouldThrowBadRequestException()
        {
            // Arrange
            var request = new CreateBiddingSessionRequest
            {
                StartTime = DateTime.Now.AddMinutes(10),
                EndTime = DateTime.Now.AddHours(1),
                OpeningValue = 10000,
                MinimumJumpingValue = 500,
                VehicleId = 1
            };

            var vehicle = new Vehicle
            {
                Id = 1,
                Status = VehicleStatus.Sold // Vehicle is not available
            };

            _unitOfWorkMock
                .Setup(uow => uow.VehicleRepository.GetVehicleByIdAsync(request.VehicleId))
                .ReturnsAsync(vehicle);  // Mock vehicle not available

            // Act & Assert
            var exception = await Assert.ThrowsAsync<BadRequestException>(() => _biddingSessionService.CreateBiddingSession(request));
            Assert.Equal(SystemConstants.BiddingSessionMessageResponses.BiddingSessionCreateFailed, exception.Message);
        }

        [Fact]
        public async Task CreateBiddingSession_InternalError_ShouldThrowInternalServerException()
        {
            // Arrange
            var request = new CreateBiddingSessionRequest
            {
                StartTime = DateTime.Now.AddMinutes(10),
                EndTime = DateTime.Now.AddHours(1),
                OpeningValue = 10000,
                MinimumJumpingValue = 500,
                VehicleId = 1
            };

            _unitOfWorkMock
                .Setup(uow => uow.VehicleRepository.GetVehicleByIdAsync(request.VehicleId))
                .Throws(new Exception("Database error"));  // Mock database error

            // Act & Assert
            var exception = await Assert.ThrowsAsync<InternalServerException>(() => _biddingSessionService.CreateBiddingSession(request));
            Assert.Equal(SystemConstants.InternalMessageResponses.InternalMessageError, exception.Message);
        }

        [Fact]
        public async Task DisableBiddingSession_ValidId_ShouldReturnSuccess()
        {
            // Arrange
            var sessionId = Guid.NewGuid();

            // Mock the disabling of the bidding session
            _unitOfWorkMock
                .Setup(uow => uow.BiddingSessionRepository.DisableBiddingSessionAsync(sessionId))
                .ReturnsAsync(true);  // Simulate successful disable

            // Act
            var response = await _biddingSessionService.DisableBiddingSession(sessionId);

            // Assert
            Assert.True(response.IsSuccess);
            Assert.Equal(StatusCodes.Status200OK, response.StatusCode);
            Assert.Equal(SystemConstants.CommonResponse.UpdateSuccess, response.Message);
        }
    }
}
