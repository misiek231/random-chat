using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using RandomChat.Xamarin.MvvmPackage.Attributes;
using RandomChat.Xamarin.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace RandomChat.Xamarin.Services
{
    [SingleInstance]
    public class ChatService : IChatService
    {

        private HubConnection _hubConnection;

        public EventHandler ChatStarted { get; set; }
        public EventHandler<NewMessageEventArgs> NewMessage { get; set; }
        public EventHandler ChatStopped { get; set; }

        public async Task StartNewChatAsync()
        {
            const string ip = "randomchat.sloppy.zone";
            _hubConnection = new HubConnectionBuilder()
                .WithUrl($"https://{ip}/chat", HttpTransportType.ServerSentEvents).Build();

            _hubConnection.On("StartChat", () => ChatStarted?.Invoke(this, EventArgs.Empty));
            _hubConnection.On<string>("Message", (message) => NewMessage?.Invoke(this, new NewMessageEventArgs(message)));
            _hubConnection.On("ClientDisconnected", () => ChatStopped?.Invoke(this, EventArgs.Empty));
            await _hubConnection.StartAsync().ConfigureAwait(false);
        }

        public Task SendMessageAsync(string messageBoxContent)
        {
            return _hubConnection.InvokeAsync("Message", messageBoxContent);
        }

        public async Task StopChatAsync()
        {
            if(_hubConnection == null)
            {
                return;
            }
            await _hubConnection.StopAsync().ConfigureAwait(false);
            await _hubConnection.DisposeAsync().ConfigureAwait(false);
            _hubConnection = null;
        }
    }
}
