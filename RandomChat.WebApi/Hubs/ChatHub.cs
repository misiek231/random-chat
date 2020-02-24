using Microsoft.AspNetCore.SignalR;
using RandomChat.WebApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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
            if (_chatsContainer.NewClient(Context.ConnectionId, out string secondClient))
            {
                await Clients.Caller.SendAsync("StartChat");
                await Clients.Client(secondClient).SendAsync("StartChat");
            }
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Clients.Client(_chatsContainer.DestroyChat(Context.ConnectionId)).SendAsync("ClientDisconnected");
            await base.OnDisconnectedAsync(exception);
        }
    }
}
