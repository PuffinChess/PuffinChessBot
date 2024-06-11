using Microsoft.AspNetCore.SignalR;
using StorkEngineApi.Models;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace StorkEngineApi.Hubs
{
    public class GameHub : Hub
    {

        private static ConcurrentQueue<string> waitingUsers = new ConcurrentQueue<string>();
        private static ConcurrentDictionary<string, string> pairedUsers = new ConcurrentDictionary<string, string>();

        public override async Task OnConnectedAsync()
        {
            waitingUsers.Enqueue(Context.ConnectionId);
            await TryPairUsers();
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            if (pairedUsers.TryRemove(Context.ConnectionId, out var pairedUser))
            {
                pairedUsers.TryRemove(pairedUser, out _);
                await Clients.Client(pairedUser).SendAsync("PairedUserDisconnected");
            }
            else
            {
                waitingUsers = new ConcurrentQueue<string>(waitingUsers.Except(new[] { Context.ConnectionId }));
            }

            await base.OnDisconnectedAsync(exception);
        }

        private async Task TryPairUsers()
        {
            if (waitingUsers.Count >= 2)
            {
                if (waitingUsers.TryDequeue(out var user1) && waitingUsers.TryDequeue(out var user2))
                {
                    pairedUsers.TryAdd(user1, user2);
                    pairedUsers.TryAdd(user2, user1);

                    // Send "white" to one user and "black" to the other

                    // Inform each user of their pairing
                    await Clients.Client(user1).SendAsync("Paired", user2);
                    await Clients.Client(user2).SendAsync("Paired", user1);

                    await Clients.Client(user1).SendAsync("ReceiveMessage", Context.ConnectionId, "white");
                    await Clients.Client(user2).SendAsync("ReceiveMessage", Context.ConnectionId, "black");
                }
            }
        }

            public async Task SendMessage(string targetConnectionId, string message)
        {
            await Clients.Client(targetConnectionId).SendAsync("ReceiveMessage", Context.ConnectionId, message);
        }
    }
}
