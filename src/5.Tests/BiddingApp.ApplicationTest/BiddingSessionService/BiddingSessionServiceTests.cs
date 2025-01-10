using BiddingApp.Infrastructure.Dtos.BiddingSessionDtos;

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

        [Fact]
        public async Task CloseBiddingSession_ValidId_ShouldReturnSuccess()
        {
            // Arrange
            var sessionId = Guid.NewGuid();
            var session = new BiddingSession
            {
                Id = sessionId,
                VehicleId = 1,
                TotalBiddingCount = 5 // Simulating there were bids on the session
            };

            // Mock GetBiddingSessionByIdAsync
            _unitOfWorkMock
                .Setup(uow => uow.BiddingSessionRepository.GetBiddingSessionByIdAsync(sessionId))
                .ReturnsAsync(session);

            // Mock CloseBiddingSessionAsync
            _unitOfWorkMock
                .Setup(uow => uow.BiddingSessionRepository.CloseBiddingSessionAsync(sessionId))
                .ReturnsAsync(true);

            // Mock UpdateVehicleStatusAsync for 'Sold' status
            _unitOfWorkMock
                .Setup(uow => uow.VehicleRepository.UpdateVehicleStatusAsync(session.VehicleId, VehicleStatus.Sold))
                .ReturnsAsync(true);

            // Act
            var response = await _biddingSessionService.CloseBiddingSession(sessionId);

            // Assert
            Assert.True(response.IsSuccess);
            Assert.Equal(StatusCodes.Status200OK, response.StatusCode);
            Assert.Equal(SystemConstants.CommonResponse.UpdateSuccess, response.Message);

            // Verify interactions
            _unitOfWorkMock.Verify(uow => uow.BiddingSessionRepository.GetBiddingSessionByIdAsync(sessionId), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.BiddingSessionRepository.CloseBiddingSessionAsync(sessionId), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.VehicleRepository.UpdateVehicleStatusAsync(session.VehicleId, VehicleStatus.Sold), Times.Once);
        }

        [Fact]
        public async Task CloseBiddingSession_NoBids_ShouldUpdateVehicleToAvailable()
        {
            // Arrange
            var sessionId = Guid.NewGuid();
            var session = new BiddingSession
            {
                Id = sessionId,
                VehicleId = 1,
                TotalBiddingCount = 0 
            };

            // Mock GetBiddingSessionByIdAsync
            _unitOfWorkMock
                .Setup(uow => uow.BiddingSessionRepository.GetBiddingSessionByIdAsync(sessionId))
                .ReturnsAsync(session);

            // Mock CloseBiddingSessionAsync
            _unitOfWorkMock
                .Setup(uow => uow.BiddingSessionRepository.CloseBiddingSessionAsync(sessionId))
                .ReturnsAsync(true);

            // Mock UpdateVehicleStatusAsync for 'Available' status
            _unitOfWorkMock
                .Setup(uow => uow.VehicleRepository.UpdateVehicleStatusAsync(session.VehicleId, VehicleStatus.Available))
                .ReturnsAsync(true);

            // Act
            var response = await _biddingSessionService.CloseBiddingSession(sessionId);

            // Assert
            Assert.True(response.IsSuccess);
            Assert.Equal(StatusCodes.Status200OK, response.StatusCode);
            Assert.Equal(SystemConstants.CommonResponse.UpdateSuccess, response.Message);

            // Verify interactions
            _unitOfWorkMock.Verify(uow => uow.BiddingSessionRepository.GetBiddingSessionByIdAsync(sessionId), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.BiddingSessionRepository.CloseBiddingSessionAsync(sessionId), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.VehicleRepository.UpdateVehicleStatusAsync(session.VehicleId, VehicleStatus.Available), Times.Once);
        }

        [Fact]
        public async Task CloseBiddingSession_SessionNotFound_ShouldThrowNotFoundException()
        {
            // Arrange
            var sessionId = Guid.NewGuid();

            // Mock GetBiddingSessionByIdAsync to return null
            _unitOfWorkMock
                .Setup(uow => uow.BiddingSessionRepository.GetBiddingSessionByIdAsync(sessionId))
                .ReturnsAsync((BiddingSession)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<NotFoundException>(() => _biddingSessionService.CloseBiddingSession(sessionId));

            Assert.Equal(SystemConstants.BiddingSessionMessageResponses.BiddingSessionNotFound, exception.Message);

            // Verify no further interactions occurred
            _unitOfWorkMock.Verify(uow => uow.BiddingSessionRepository.CloseBiddingSessionAsync(It.IsAny<Guid>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.VehicleRepository.UpdateVehicleStatusAsync(1, It.IsAny<VehicleStatus>()), Times.Never);
        }

        [Fact]
        public async Task GetBiddingSessionById_ValidId_ShouldReturnBiddingSessionVm()
        {
            // Arrange
            var sessionId = Guid.NewGuid();
            var biddingSession = new BiddingSession
            {
                Id = sessionId,
                StartTime = DateTime.UtcNow.AddDays(-1),
                EndTime = DateTime.UtcNow.AddDays(1),
                HighestBidding = 1000,
                MinimumJumpingValue = 100,
                VehicleId = 1,
                TotalBiddingCount = 5
            };

            var biddingSessionVm = new BiddingSessionVm
            {
                Id = biddingSession.Id,
                StartTime = biddingSession.StartTime,
                EndTime = biddingSession.EndTime,
                HighestBidding = biddingSession.HighestBidding,
                MinimumJumpingValue = biddingSession.MinimumJumpingValue,
                VehicleId = biddingSession.VehicleId,
                TotalBiddingCount = biddingSession.TotalBiddingCount
            };

            // Mock repository
            _unitOfWorkMock
                .Setup(uow => uow.BiddingSessionRepository.GetBiddingSessionByIdAsync(sessionId))
                .ReturnsAsync(biddingSession);

            // Mock mapper
            _mapperMock
                .Setup(mapper => mapper.Map<BiddingSessionVm>(biddingSession))
                .Returns(biddingSessionVm);

            // Act
            var response = await _biddingSessionService.GetBiddingSessionById(sessionId);

            // Assert
            Assert.True(response.IsSuccess);
            Assert.Equal(StatusCodes.Status200OK, response.StatusCode);
            Assert.Equal(SystemConstants.CommonResponse.FetchSuccess, response.Message);
            Assert.NotNull(response.Data);
            Assert.Equal(sessionId, response.Data.Id);

            // Verify interactions
            _unitOfWorkMock.Verify(uow => uow.BiddingSessionRepository.GetBiddingSessionByIdAsync(sessionId), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<BiddingSessionVm>(biddingSession), Times.Once);
        }

        [Fact]
        public async Task GetBiddingSessionById_InvalidId_ShouldThrowNotFoundException()
        {
            // Arrange
            var sessionId = Guid.NewGuid();

            // Mock repository to return null
            _unitOfWorkMock
                .Setup(uow => uow.BiddingSessionRepository.GetBiddingSessionByIdAsync(sessionId))
                .ReturnsAsync((BiddingSession)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<NotFoundException>(() => _biddingSessionService.GetBiddingSessionById(sessionId));

            Assert.Equal(SystemConstants.BiddingSessionMessageResponses.BiddingSessionNotFound, exception.Message);

            // Verify interactions
            _unitOfWorkMock.Verify(uow => uow.BiddingSessionRepository.GetBiddingSessionByIdAsync(sessionId), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<BiddingSessionVm>(It.IsAny<BiddingSession>()), Times.Never);
        }
        [Fact]
        public async Task GetAllBiddingSessions_ValidRequest_ShouldReturnPagedResult()
        {
            // Arrange
            var filter = new BiddingSessionFilter
            {
                PageNumber = 1,
                PageSize = 10,
                IsActive = true,
                StartTime = DateTime.UtcNow.AddDays(-7),
                EndTime = DateTime.UtcNow.AddDays(7),
                VIN = "12345VIN"
            };

            var biddingSessions = new List<BiddingSession>
            {
                new BiddingSession
                {
                    Id = Guid.NewGuid(),
                    StartTime = DateTime.UtcNow.AddDays(-1),
                    EndTime = DateTime.UtcNow.AddDays(1),
                    HighestBidding = 1000,
                    MinimumJumpingValue = 100,
                    VehicleId = 1,
                    TotalBiddingCount = 5
                },
                new BiddingSession
                {
                    Id = Guid.NewGuid(),
                    StartTime = DateTime.UtcNow.AddDays(-2),
                    EndTime = DateTime.UtcNow.AddDays(2),
                    HighestBidding = 2000,
                    MinimumJumpingValue = 200,
                    VehicleId = 2,
                    TotalBiddingCount = 10
                }
            };

            var biddingSessionVms = biddingSessions.Select(session => new BiddingSessionVm
            {
                Id = session.Id,
                StartTime = session.StartTime,
                EndTime = session.EndTime,
                HighestBidding = session.HighestBidding,
                MinimumJumpingValue = session.MinimumJumpingValue,
                VehicleId = session.VehicleId,
                TotalBiddingCount = session.TotalBiddingCount
            }).ToList();

            var pagingResult = new BiddingSessionResult
            {
                BiddingSessions = biddingSessions,
                TotalItems = 2,
                ItemCounts = 10
            };

            // Mock repository
            _unitOfWorkMock
                .Setup(uow => uow.BiddingSessionRepository.GetAllBiddingSessionsAsync(filter))
                .ReturnsAsync(pagingResult);

            // Mock mapper
            _mapperMock
                .Setup(mapper => mapper.Map<List<BiddingSessionVm>>(biddingSessions))
                .Returns(biddingSessionVms);

            // Act
            var response = await _biddingSessionService.GetAllBiddingSessions(filter);

            // Assert
            Assert.True(response.IsSuccess);
            Assert.Equal(StatusCodes.Status200OK, response.StatusCode);
            Assert.Equal(SystemConstants.CommonResponse.FetchSuccess, response.Message);
            Assert.NotNull(response.Data);
            Assert.Equal(filter.PageNumber, response.Data.PageNumber);
            Assert.Equal(filter.PageSize, response.Data.PageSize);
            Assert.Equal(2, response.Data.TotalItems);
            Assert.Equal(10, response.Data.ItemCounts);
            Assert.Equal(biddingSessionVms.Count, response.Data.Items.Count);

            // Verify interactions
            _unitOfWorkMock.Verify(uow => uow.BiddingSessionRepository.GetAllBiddingSessionsAsync(filter), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<List<BiddingSessionVm>>(biddingSessions), Times.Once);
        }

        [Fact]
        public async Task GetAllBiddingByUserId_ValidRequest_ShouldReturnPagedResult()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var filter = new UserBiddingSessionFilter
            {
                PageNumber = 1,
                PageSize = 10,
                StartTime = DateTime.UtcNow.AddDays(-7),
                EndTime = DateTime.UtcNow.AddDays(7),
                VIN = "12345VIN"
            };

            var biddingSessions = new List<BiddingSession>
            {
                new BiddingSession
                {
                    Id = Guid.NewGuid(),
                    StartTime = DateTime.UtcNow.AddDays(-1),
                    EndTime = DateTime.UtcNow.AddDays(1),
                    HighestBidding = 1000,
                    MinimumJumpingValue = 100,
                    VehicleId = 1,
                    TotalBiddingCount = 5
                },
                new BiddingSession
                {
                    Id = Guid.NewGuid(),
                    StartTime = DateTime.UtcNow.AddDays(-2),
                    EndTime = DateTime.UtcNow.AddDays(2),
                    HighestBidding = 2000,
                    MinimumJumpingValue = 200,
                    VehicleId = 2,
                    TotalBiddingCount = 10
                }
            };

            var biddingSessionVms = biddingSessions.Select(session => new BiddingSessionVm
            {
                Id = session.Id,
                StartTime = session.StartTime,
                EndTime = session.EndTime,
                HighestBidding = session.HighestBidding,
                MinimumJumpingValue = session.MinimumJumpingValue,
                VehicleId = session.VehicleId,
                TotalBiddingCount = session.TotalBiddingCount
            }).ToList();

            var pagingResult = new BiddingSessionResult
            {
                BiddingSessions = biddingSessions,
                TotalItems = 2,
                ItemCounts = 10
            };

            // Mock repository
            _unitOfWorkMock
                .Setup(uow => uow.BiddingSessionRepository.GetAllBiddingSessionByUserIdAsync(userId, filter))
                .ReturnsAsync(pagingResult);

            // Mock mapper
            _mapperMock
                .Setup(mapper => mapper.Map<List<BiddingSessionVm>>(biddingSessions))
                .Returns(biddingSessionVms);

            // Act
            var response = await _biddingSessionService.GetAllBiddingSessionsWithUserState(userId, filter);

            // Assert
            Assert.True(response.IsSuccess);
            Assert.Equal(StatusCodes.Status200OK, response.StatusCode);
            Assert.Equal(SystemConstants.CommonResponse.FetchSuccess, response.Message);
            Assert.NotNull(response.Data);
            Assert.Equal(filter.PageNumber, response.Data.PageNumber);
            Assert.Equal(filter.PageSize, response.Data.PageSize);
            Assert.Equal(2, response.Data.TotalItems);
            Assert.Equal(10, response.Data.ItemCounts);
            Assert.Equal(biddingSessionVms.Count, response.Data.Items.Count);

            // Verify interactions
            _unitOfWorkMock.Verify(uow => uow.BiddingSessionRepository.GetAllBiddingSessionByUserIdAsync(userId, filter), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<List<BiddingSessionVm>>(biddingSessions), Times.Once);
        }
    }
}
