using BiddingApp.Application.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace BiddingApp.Application.SignalRServices
{
    public class BiddingNotificationService : IBiddingNotificationService
    {
        private readonly IHubContext<BiddingHub> _hubContext;

        public BiddingNotificationService(IHubContext<BiddingHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task NotifyBiddingUpdateAsync(Guid sessionId, Guid userId, decimal bidValue)
        {
            await _hubContext.Clients.Group(sessionId.ToString())
                .SendAsync("ReceiveBid", new
                {
                    UserId = userId,
                    BiddingSessionId = sessionId,
                    UserCurrentBidding = bidValue,
                });
        }

        public async Task NotifyAllSessionAsync(Guid sessionId)
        {
            await _hubContext.Clients.All.SendAsync("SessionListUpdated", new
            {
                sessionId = sessionId,
                Message = "Session list updated"
            });
        }
    }
}