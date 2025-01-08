using BiddingApp.Application.SignalRServices;
using BiddingApp.Infrastructure.Dtos.BiddingDtos;

namespace BiddingApp.ApplicationTest.BiddingService
{
    public class BiddingServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<ILogger<BiddingApp.Application.Services.BiddingServices.BiddingService>> _loggerMock;
        private readonly Mock<IBiddingNotificationService> _notificationServiceMock;
        private readonly BiddingApp.Application.Services.BiddingServices.BiddingService _biddingService;

        public BiddingServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _loggerMock = new Mock<ILogger<BiddingApp.Application.Services.BiddingServices.BiddingService>>();
            _notificationServiceMock = new Mock<IBiddingNotificationService>();
            _biddingService = new BiddingApp.Application.Services.BiddingServices.BiddingService(
                _unitOfWorkMock.Object,
                _loggerMock.Object,
                _notificationServiceMock.Object);
        }

        [Fact]
        public async Task CreateBidding_ShouldReturnSuccess_WhenValidRequest()
        {
            // Arrange
            var request = new CreateBiddingRequest
            {
                BiddingSessionId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                UserCurrentBidding = 500
            };

            var session = new BiddingSession { Id = request.BiddingSessionId, HighestBidding = 400, IsClosed = false, TotalBiddingCount = 0 };
            var user = new User { Id = request.UserId, Budget = 1000 };

            _unitOfWorkMock.Setup(u => u.BiddingSessionRepository.GetBiddingSessionByIdAsync(request.BiddingSessionId))
                .ReturnsAsync(session);

            _unitOfWorkMock.Setup(u => u.UserRepository.GetUserByIdAsync(request.UserId))
                .ReturnsAsync(user);

            _unitOfWorkMock.Setup(u => u.BidRepository.UpdateUserStateAsync(It.IsAny<Guid>()))
                .ReturnsAsync(true); // Mock successful update

            _unitOfWorkMock.Setup(u => u.BiddingSessionRepository.FetchBiddingAsync(It.IsAny<Guid>(), It.IsAny<decimal>()))
                .ReturnsAsync(true); // Mock successful bidding fetch

            _unitOfWorkMock.Setup(u => u.BidRepository.CreateBiddingRequestAsync(request))
                .ReturnsAsync(true);

            _notificationServiceMock.Setup(n => n.NotifyBiddingUpdateAsync(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<decimal>()))
                .Returns(Task.CompletedTask);

            // Act
            var response = await _biddingService.CreateBidding(request);

            // Assert
            Assert.True(response.IsSuccess);
            Assert.Equal(SystemConstants.BiddingMessageResponses.BiddingCreated, response.Message);
        }

        [Fact]
        public async Task CreateBidding_ShouldThrowNotFoundException_WhenSessionNotFound()
        {
            // Arrange
            var request = new CreateBiddingRequest { BiddingSessionId = Guid.NewGuid() };
            _unitOfWorkMock.Setup(u => u.BiddingSessionRepository.GetBiddingSessionByIdAsync(request.BiddingSessionId))
                .ReturnsAsync((BiddingSession)null);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _biddingService.CreateBidding(request));
        }

        [Fact]
        public async Task CreateBidding_ShouldReturnError_WhenSessionIsClosed()
        {
            // Arrange
            var request = new CreateBiddingRequest { BiddingSessionId = Guid.NewGuid() };
            var session = new BiddingSession { Id = request.BiddingSessionId, IsClosed = true };

            _unitOfWorkMock.Setup(u => u.BiddingSessionRepository.GetBiddingSessionByIdAsync(request.BiddingSessionId))
                .ReturnsAsync(session);

            // Act
            var response = await _biddingService.CreateBidding(request);

            // Assert
            Assert.False(response.IsSuccess);
            Assert.Equal(SystemConstants.BiddingSessionMessageResponses.BiddingSessionClosed, response.Message);
        }

        [Fact]
        public async Task CreateBidding_ShouldReturnError_WhenBidValueIsInvalid()
        {
            // Arrange
            var request = new CreateBiddingRequest { BiddingSessionId = Guid.NewGuid(), UserCurrentBidding = 300 };
            var session = new BiddingSession { Id = request.BiddingSessionId, HighestBidding = 400, IsClosed = false };

            _unitOfWorkMock.Setup(u => u.BiddingSessionRepository.GetBiddingSessionByIdAsync(request.BiddingSessionId))
                .ReturnsAsync(session);

            // Act
            var response = await _biddingService.CreateBidding(request);

            // Assert
            Assert.False(response.IsSuccess);
            Assert.Equal(SystemConstants.BiddingMessageResponses.BiddingNotValid, response.Message);
        }

        [Fact]
        public async Task CreateBidding_ShouldReturnError_WhenUserBudgetIsInsufficient()
        {
            // Arrange
            var request = new CreateBiddingRequest { BiddingSessionId = Guid.NewGuid(), UserId = Guid.NewGuid(), UserCurrentBidding = 500 };
            var session = new BiddingSession { Id = request.BiddingSessionId, HighestBidding = 400, IsClosed = false };
            var user = new User { Id = request.UserId, Budget = 300 };

            _unitOfWorkMock.Setup(u => u.BiddingSessionRepository.GetBiddingSessionByIdAsync(request.BiddingSessionId))
                .ReturnsAsync(session);
            _unitOfWorkMock.Setup(u => u.UserRepository.GetUserByIdAsync(request.UserId))
                .ReturnsAsync(user);

            // Act
            var response = await _biddingService.CreateBidding(request);

            // Assert
            Assert.False(response.IsSuccess);
            Assert.Equal(SystemConstants.AuthenticateResponses.UserBudgetCheck, response.Message);
        }
    }
}
