using BiddingApp.Infrastructure.Repositories.BiddingSessionRepositories;
using BiddingApp.Infrastructure.Repositories.BiddingRepositories;
using BiddingApp.Infrastructure.Repositories.UserRepositories;
using BiddingApp.Infrastructure.Repositories.VehicleRepositories;
using BiddingApp.Infrastructure.Repositories.AuthenticateRepositories;

namespace BiddingApp.Infrastructure
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IVehicleRepository VehicleRepository { get; }
        IBiddingRepository BidRepository { get; }
        IBiddingSessionRepository BiddingSessionRepository { get; }
        IAuthenticateRepository AuthenticateRepository { get; }
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
        Task SaveChangesAsync();
    }
}
