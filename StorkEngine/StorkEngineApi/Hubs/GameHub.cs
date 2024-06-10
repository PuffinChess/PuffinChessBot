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

                    await Clients.Client(user1).SendAsync("Paired", "white");
                    await Clients.Client(user2).SendAsync("Paired", "black");
                }
            }
        }

        public async Task SendMove(string move)
        {
            if (pairedUsers.TryGetValue(Context.ConnectionId, out var pairedUser))
            {
                await Clients.Client(pairedUser).SendAsync("ReceiveMove", move);
            }
        }
    }
}
