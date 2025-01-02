using BiddingApp.BuildingBlock.Exceptions;
using BiddingApp.Domain.Models.EF;
using BiddingApp.Infrastructure.Repositories.BiddingSessionRepositories;
using BiddingApp.Infrastructure.Repositories.BiddingRepositories;
using BiddingApp.Infrastructure.Repositories.UserRepositories;
using BiddingApp.Infrastructure.Repositories.VehicleRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using BiddingApp.Infrastructure.Repositories.AuthenticateRepositories;

namespace BiddingApp.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private IUserRepository? _userRepository;
        private IVehicleRepository? _vehicleRepository;
        private IBiddingRepository? _bidRepository;
        private IBiddingSessionRepository? _biddingSessionRepository;
        private IAuthenticateRepository? _authenticateRepository;
        private readonly ILogger<UnitOfWork> _logger;

        // Constructor without injecting specific repositories
        public UnitOfWork(ApplicationDbContext dbContext, ILogger<UnitOfWork> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        // Lazy initialization for repositories
        public IUserRepository UserRepository =>
            _userRepository ??= new UserRepository(_dbContext);
        public IVehicleRepository VehicleRepository => 
            _vehicleRepository ??= new VehicalRepository(_dbContext);

        public IBiddingRepository BidRepository => 
            _bidRepository ??= new BiddingRepository(_dbContext);

        public IBiddingSessionRepository BiddingSessionRepository => 
            _biddingSessionRepository ??= new BiddingSessionRepository(_dbContext);
        public IAuthenticateRepository AuthenticateRepository =>
           _authenticateRepository ??= new AuthenticateRepository(_dbContext);
        public async Task SaveChangesAsync()
        {
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new BadRequestException("Error saving changes.", ex.Message);
            }
        }

        // Begin a transaction and manage the scope for pessimistic locking
        public async Task BeginTransactionAsync()
        {
            await _dbContext.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await _dbContext.Database.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                await _dbContext.Database.RollbackTransactionAsync();
                _logger.LogError(ex, "An error occurred while committing changes to the database.");
            }
        }

        public async Task RollbackTransactionAsync()
        {
            await _dbContext.Database.RollbackTransactionAsync();
            _logger.LogError("An error occurred while committing changes to the database.");
        }

        // Dispose the database context and transaction when done
        public void Dispose()
        {
            _dbContext?.Dispose();
            _logger.LogInformation("Disposed UnitOfWork and Database context.");
        }
    }
}
