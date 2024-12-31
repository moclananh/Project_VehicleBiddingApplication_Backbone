using Microsoft.AspNetCore.SignalR;

namespace BiddingApp.Application.Hubs
{
    public class BiddingHub : Hub
    {
        // Called when a client connects
        public override Task OnConnectedAsync()
        {
            Console.WriteLine($"Connection established: {Context.ConnectionId}");
            return base.OnConnectedAsync();
        }

        // Called when a client disconnects
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            Console.WriteLine($"Connection closed: {Context.ConnectionId}");
            return base.OnDisconnectedAsync(exception);
        }

        // Join a group (session)
        public async Task JoinBiddingSession(string sessionId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, sessionId);
            await Clients.Group(sessionId).SendAsync("UserJoined", Context.ConnectionId);
        }

        // Leave a group (session)
        public async Task LeaveBiddingSession(string sessionId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, sessionId);
            await Clients.Group(sessionId).SendAsync("UserLeft", Context.ConnectionId);
        }
    }
}
