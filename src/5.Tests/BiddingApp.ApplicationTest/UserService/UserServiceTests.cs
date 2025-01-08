using BiddingApp.Infrastructure.Repositories.UserRepositories;

namespace BiddingApp.ApplicationTest.UserService
{
    public class UserServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ILogger<BiddingApp.Application.Services.UserServices.UserService>> _loggerMock;
        private readonly BiddingApp.Application.Services.UserServices.UserService _userService;

        public UserServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _loggerMock = new Mock<ILogger<BiddingApp.Application.Services.UserServices.UserService>>();
            _userService = new BiddingApp.Application.Services.UserServices.UserService(_unitOfWorkMock.Object, _mapperMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetUserById_ValidId_ShouldReturnUser()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new User { Id = userId, Username = "John Doe", Email = "john@example.com", Role = UserRole.Dealer, Budget = 1000 };

            _unitOfWorkMock
                .Setup(x => x.UserRepository.GetUserByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(user);

            _mapperMock
                .Setup(x => x.Map<UserVm>(It.IsAny<User>()))
                .Returns(new UserVm
                {
                    Id = userId,
                    UserName = "John Doe",
                    Email = "john@example.com",
                    Role = UserRole.Dealer,
                    Budget = 1000
                });

            // Act
            var response = await _userService.GetUserByid(userId);

            // Assert
            Assert.True(response.IsSuccess);
            Assert.NotNull(response.Data);
            Assert.Equal("John Doe", response.Data.UserName);
            Assert.Equal(StatusCodes.Status200OK, response.StatusCode);
        }

        [Fact]
        public async Task GetUserById_InvalidId_ShouldThrowNotFoundException()
        {
            // Arrange
            var userId = Guid.NewGuid();

            _unitOfWorkMock
                .Setup(x => x.UserRepository.GetUserByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((User?)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<NotFoundException>(() => _userService.GetUserByid(userId));
            Assert.Equal(SystemConstants.AuthenticateResponses.UserNotExist, exception.Message);
        }

        [Fact]
        public async Task GetUserReport_ValidRequest_ShouldReturnPagingResult()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var request = new UserReportFilter { PageNumber = 1, PageSize = 10 };

            // Create mock UserReportVm list
            var reports = new List<UserReportVm>
            {
                new UserReportVm
                {
                    BiddingId = 1,
                    UserCurrentBidding = 500,
                    BiddingAt = DateTime.Now,
                    IsWinner = true,
                    SessionId = Guid.NewGuid(),
                    StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddHours(1),
                    IsClosed = true,
                    VehicleName = "Car 1",
                    VIN = "VIN123",
                    ImageUrl = "https://example.com/image1.jpg"
                }
            };

            // Create mock UserReportResult
            var userReportResult = new UserReportResult
            {
                Reports = reports,
                TotalItems = 1,
                ItemCounts = 1
            };

            // Mock the IUserRepository.GetUserReportAsync method
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock
                .Setup(x => x.GetUserReportAsync(It.IsAny<Guid>(), It.IsAny<UserReportFilter>()))
                .ReturnsAsync(userReportResult);

            // Mock the UnitOfWork to return the mocked UserRepository
            _unitOfWorkMock
                .Setup(x => x.UserRepository)
                .Returns(userRepositoryMock.Object);

            // Mock AutoMapper to return a list of UserReportVm
            _mapperMock
                .Setup(x => x.Map<List<UserReportVm>>(It.IsAny<List<UserReportVm>>()))
                .Returns(reports);

            // Act
            var response = await _userService.GetUserReport(userId, request);

            // Assert
            Assert.True(response.IsSuccess);
            Assert.NotNull(response.Data);
            Assert.Equal(1, response.Data.Items.Count);
            Assert.Equal(StatusCodes.Status200OK, response.StatusCode);
        }
    }
}
