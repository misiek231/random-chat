using RandomChat.Xamarin.MvvmPackage;
using RandomChat.Xamarin.MvvmPackage.Commands;
using RandomChat.Xamarin.MvvmPackage.Services.Interfaces;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RandomChat.Xamarin.PageModels
{
    public class MainPageModel : PageModelBase
    {
        public ICommand StartChat { get; set; }
        private readonly INavigationService _navigationService;

        public MainPageModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            StartChat = new AsyncCommand(ExecuteStartChat);
        }

        private async Task ExecuteStartChat()
        {
            await _navigationService.NavigateToAsync<ChatPageModel>();
        }
    }
}
