namespace BiddingApp.ApplicationTest.AuthenticateService
{
    public class AuthenticateServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ILogger<BiddingApp.Application.Services.AuthenticateServices.AuthenticateService>> _loggerMock;
        private readonly IOptions<AppSetting> _appSettings;
        private readonly BiddingApp.Application.Services.AuthenticateServices.AuthenticateService _authService;

        public AuthenticateServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _loggerMock = new Mock<ILogger<BiddingApp.Application.Services.AuthenticateServices.AuthenticateService>>();
            _appSettings = Options.Create(new AppSetting { SecretKey = "TFB7q6pXxPjDckZdJGVbfeKyEhsrH24n" });
            _authService = new BiddingApp.Application.Services.AuthenticateServices.AuthenticateService(
                _unitOfWorkMock.Object,
                _appSettings,
                _mapperMock.Object,
                _loggerMock.Object);
        }

        [Fact]
        public async Task Authenticate_ShouldReturnSuccess_WhenValidRequest()
        {
            // Arrange
            var request = new LoginVm { Email = "testuser@example.com", Password = "correctpassword" };

            // Generate a hashed password using the PasswordHasher
            var passwordHasher = new PasswordHasher<LoginVm>();
            var hashedPassword = passwordHasher.HashPassword(request, "correctpassword");

            // Simulate repository response (successful authentication)
            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = "testuser",
                Email = "testuser@example.com",
                Role = Domain.Models.Enums.UserRole.Dealer,
                Budget = 1000.00m,
                Password = hashedPassword
            };

            var authResponse = new AuthenticateResponse { Result = 1, User = user };

            _unitOfWorkMock.Setup(u => u.AuthenticateRepository.AuthenticateUser(request))
                .ReturnsAsync(authResponse);

            _mapperMock.Setup(m => m.Map<UserVm>(user))
                .Returns(new UserVm
                {
                    Id = user.Id,
                    UserName = user.Username,
                    Email = user.Email,
                    Budget = user.Budget,
                    Role = user.Role
                });

            // Act
            var response = await _authService.Authencate(request);

            // Assert
            Assert.True(response.IsSuccess);
            Assert.Equal(SystemConstants.AuthenticateResponses.UserAuthenticated, response.Message);
            Assert.Equal(StatusCodes.Status200OK, response.StatusCode);
            Assert.NotNull(response.Data);
            Assert.Equal(user.Email, response.Data.Email);
            Assert.Equal(user.Username, response.Data.UserName);
            Assert.Equal(user.Budget, response.Data.Budget);
            Assert.Equal(user.Role.ToString(), response.Data.Role);
        }

        [Fact]
        public async Task Authenticate_ShouldThrowNotFoundException_WhenUserDoesNotExist()
        {
            // Arrange
            var request = new LoginVm { Email = "nonexistentuser@example.com", Password = "wrongpassword" };

            // Simulate repository response (user not found)
            _unitOfWorkMock.Setup(u => u.AuthenticateRepository.AuthenticateUser(request))
                .ReturnsAsync(new AuthenticateResponse { Result = -1, User = null });

            // Act & Assert
            var exception = await Assert.ThrowsAsync<NotFoundException>(() => _authService.Authencate(request));
            Assert.Equal(SystemConstants.AuthenticateResponses.UserNotExist, exception.Message);
        }

        [Fact]
        public async Task Authenticate_ShouldThrowBadRequestException_WhenIncorrectPassword()
        {
            // Arrange
            var request = new LoginVm { Email = "testuser@example.com", Password = "incorrectpassword" };

            // Generate a hashed password using the PasswordHasher
            var passwordHasher = new PasswordHasher<LoginVm>();
            var hashedPassword = passwordHasher.HashPassword(request, "correctpassword");

            // Simulate repository response (valid user, incorrect password)
            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = "testuser",
                Email = "testuser@example.com",
                Role = Domain.Models.Enums.UserRole.Dealer,
                Budget = 1000.00m,
                Password = hashedPassword 
            };

            var authResponse = new AuthenticateResponse { Result = 1, User = user };

            _unitOfWorkMock.Setup(u => u.AuthenticateRepository.AuthenticateUser(request))
                .ReturnsAsync(authResponse);

            // Mock password verification failure
            var passwordHasherForVerification = new PasswordHasher<LoginVm>();
            var verificationResult = passwordHasherForVerification.VerifyHashedPassword(request, user.Password, request.Password);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<BadRequestException>(() => _authService.Authencate(request));
            Assert.Equal(SystemConstants.AuthenticateResponses.IncorrectPassword, exception.Message);
        }

        [Fact]
        public async Task Register_ShouldThrowBadRequestException_WhenEmailAlreadyTaken()
        {
            // Arrange
            var request = new RegisterVm
            {
                UserName = "testuser",
                Email = "testuser@example.com",
                Password = "password123",
                Role = UserRole.Dealer,
                Budget = 1000.00m
            };

            // Simulate repository response for email already taken
            _unitOfWorkMock.Setup(u => u.AuthenticateRepository.RegisterUser(request))
                .ReturnsAsync(-1); // Email already taken

            // Act & Assert
            var exception = await Assert.ThrowsAsync<BadRequestException>(() => _authService.Register(request));
            Assert.Equal(SystemConstants.AuthenticateResponses.EmailChecked, exception.Message);
        }

        [Fact]
        public async Task Register_ShouldThrowBadRequestException_WhenUsernameAlreadyTaken()
        {
            // Arrange
            var request = new RegisterVm
            {
                UserName = "testuser",
                Email = "newuser@example.com",
                Password = "password123",
                Role = UserRole.Dealer,
                Budget = 1000.00m
            };

            // Simulate repository response for username already taken
            _unitOfWorkMock.Setup(u => u.AuthenticateRepository.RegisterUser(request))
                .ReturnsAsync(-2); // Username already taken

            // Act & Assert
            var exception = await Assert.ThrowsAsync<BadRequestException>(() => _authService.Register(request));
            Assert.Equal(SystemConstants.AuthenticateResponses.UsernameChecked, exception.Message);
        }

        [Fact]
        public async Task Register_ShouldReturnSuccess_WhenUserRegistered()
        {
            // Arrange
            var request = new RegisterVm
            {
                UserName = "testuser",
                Email = "testuser@example.com",
                Password = "password123",
                Role = UserRole.Dealer,
                Budget = 1000.00m
            };

            // Simulate repository response for successful registration
            _unitOfWorkMock.Setup(u => u.AuthenticateRepository.RegisterUser(request))
                .ReturnsAsync(1); // Successful registration

            // Act
            var response = await _authService.Register(request);

            // Assert
            Assert.True(response.IsSuccess);
            Assert.Equal(SystemConstants.AuthenticateResponses.UserRegistered, response.Message);
            Assert.Equal(StatusCodes.Status201Created, response.StatusCode);
        }

    }
}
