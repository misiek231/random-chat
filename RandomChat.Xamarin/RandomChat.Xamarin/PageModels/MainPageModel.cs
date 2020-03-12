using PropertyChanged;
using RandomChat.Xamarin.MvvmPackage;
using RandomChat.Xamarin.MvvmPackage.Services.Interfaces;
using RandomChat.Xamarin.Services.Interfaces;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RandomChat.Xamarin.PageModels
{
    [AddINotifyPropertyChangedInterface]
    public class MainPageModel : PageModelBase
    {
        public Command StartChat { get; set; }

        public bool Searching
        {
            get => searching;
            set
            {
                searching = value;
                StartChat.ChangeCanExecute();
            }
        }

        private readonly INavigationService _navigationService;
        private readonly IChatService _chatService;
        private bool searching;

        public MainPageModel(INavigationService navigationService, IChatService chatService)
        {
            _navigationService = navigationService;
            _chatService = chatService;
            chatService.ChatStarted += async (s, e) => await ChatStarted().ConfigureAwait(false);
            StartChat = new Command(ExecuteStartChat, () => !Searching);
        }

        public override Task OnAppearingAsync()
        {
            Searching = false;
            return base.OnAppearingAsync();
        }

        private Task ChatStarted()
        {
            return _navigationService.NavigateToAsync<ChatPageModel>();
        }

        private async void ExecuteStartChat()
        {
            Searching = true;
            await _chatService.StartNewChatAsync().ConfigureAwait(false);
        }
    }
}
