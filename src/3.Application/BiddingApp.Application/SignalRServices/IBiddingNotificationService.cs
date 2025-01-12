namespace BiddingApp.Application.SignalRServices
{
    public interface IBiddingNotificationService
    {
        Task NotifyBiddingUpdateAsync(Guid sessionId, Guid userId, decimal bidValue);
        Task NotifyAllSessionAsync(Guid sessionId);
    }
}