using BiddingApp.Infrastructure.Repositories.BiddingSessionRepository;
using BiddingApp.Infrastructure.Repositories.BiddingRepositories;
using BiddingApp.Infrastructure.Repositories.UserRepositories;
using BiddingApp.Infrastructure.Repositories.VehicleRepositories;

namespace BiddingApp.Infrastructure
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IVehicleRepository VehicleRepository { get; }
        IBiddingRepository BidRepository { get; }
        IBiddingSessionRepository BiddingSessionRepository { get; }

        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
        Task SaveChangesAsync();
    }
}
