using Microsoft.AspNetCore.SignalR;
using RandomChat.WebApi.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace RandomChat.WebApi.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IChatsContainer _chatsContainer;

        public ChatHub(IChatsContainer chatsContainer)
        {
            _chatsContainer = chatsContainer;
        }

        public async Task Message(string message)
        {
            await Clients.Client(_chatsContainer.GetTargetClient(Context.ConnectionId)).SendAsync("Message", message);
        }

        public override async Task OnConnectedAsync()
        {
            Console.WriteLine("OnConnectedAsync");
            if (_chatsContainer.NewClient(Context.ConnectionId, out string secondClient))
            {
                await Clients.Caller.SendAsync("StartChat");
                await Clients.Client(secondClient).SendAsync("StartChat");
            }
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            Console.WriteLine("Client Disconnected");
            string id = _chatsContainer.DisconnectClientAndGetSecondChatter(Context.ConnectionId);
            if (id != null)
            {
                Console.WriteLine("DestroyChat");
                await Clients.Client(id).SendAsync("ClientDisconnected").ConfigureAwait(false);
                Context.Abort();
            }
            await base.OnDisconnectedAsync(exception).ConfigureAwait(false);
        }
    }
}
