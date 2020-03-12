using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RandomChat.Xamarin.Services.Interfaces
{
    public interface IChatService
    {
        EventHandler ChatStarted { get; set; }
        EventHandler<NewMessageEventArgs> NewMessage { get; set; }
        EventHandler ChatStopped { get; set; }

        Task SendMessageAsync(string messageBoxContent);
        Task StartNewChatAsync();
        Task StopChatAsync();
    }

    public class NewMessageEventArgs : EventArgs
    {
        public string Message { get; }

        public NewMessageEventArgs(string message)
        {
            Message = message;
        }
    }
}
