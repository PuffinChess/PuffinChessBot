using Microsoft.AspNetCore.SignalR;
using StorkEngineApi.Models;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace StorkEngineApi.Hubs
{
    public class GameHub : Hub
    {
        private static ConcurrentDictionary<string, string> waitingUsers = new ConcurrentDictionary<string, string>();

        public override async Task OnConnectedAsync()
        {
            waitingUsers.TryAdd(Context.ConnectionId, null);
            await TryPairUsers();
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            waitingUsers.TryRemove(Context.ConnectionId, out _);
            await base.OnDisconnectedAsync(exception);
        }

        private async Task TryPairUsers()
        {
            var availableUsers = waitingUsers.Keys.ToArray();

            if (availableUsers.Length >= 2)
            {
                var user1 = availableUsers[0];
                var user2 = availableUsers[1];

                waitingUsers.TryRemove(user1, out _);
                waitingUsers.TryRemove(user2, out _);

                await Clients.Client(user1).SendAsync("Paired", user2);
                await Clients.Client(user2).SendAsync("Paired", user1);

            }
        }

        public async Task PerformMove(string targetConnectionId, string move)
        {
            await Clients.Client(targetConnectionId).SendAsync("RecievedMessage", Context.ConnectionId, move);
        }
    }
}
