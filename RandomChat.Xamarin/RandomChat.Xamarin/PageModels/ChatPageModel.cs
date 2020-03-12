using PropertyChanged;
using RandomChat.Xamarin.Models;
using RandomChat.Xamarin.MvvmPackage;
using RandomChat.Xamarin.MvvmPackage.Commands;
using RandomChat.Xamarin.MvvmPackage.Services.Interfaces;
using RandomChat.Xamarin.Services.Interfaces;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;

namespace RandomChat.Xamarin.PageModels
{
    [AddINotifyPropertyChangedInterface]
    public class ChatPageModel : PageModelBase
    {
        private readonly IChatService _chatService;
        private readonly INavigationService _navigationService;
        public ObservableCollection<Message> Messages { get; set; }
        public string MessageBoxContent { get; set; }
        public ICommand SendCommand { get; set; }

        public ChatPageModel(IChatService chatService, INavigationService navigationService)
        {
            _navigationService = navigationService;
            _chatService = chatService;
            Messages = new ObservableCollection<Message>();
            SendCommand = new AsyncCommand(SendAction);

            chatService.NewMessage += async (s, e) => await NewMessage(e).ConfigureAwait(false);
            chatService.ChatStopped += async (s, e) => await ChatStopped().ConfigureAwait(false);
        }

        private Task ChatStopped()
        {
            return _navigationService.PopAsync();
        }

        public override async Task OnDisappearingAsync()
        {
            await _chatService.StopChatAsync().ConfigureAwait(false);
        }

        private Task NewMessage(NewMessageEventArgs message)
        {
            return MainThread.InvokeOnMainThreadAsync(() =>
                Messages.Add(new Message
                {
                    Content = message.Message,
                    IsMain = false
                }));
        }

        private async Task SendAction()
        {
            Messages.Add(new Message
            {
                Content = MessageBoxContent,
                IsMain = true
            });

            await _chatService.SendMessageAsync(MessageBoxContent).ConfigureAwait(false);
        }
    }
}
