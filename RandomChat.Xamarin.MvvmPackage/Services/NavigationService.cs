using RandomChat.Xamarin.MvvmPackage.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RandomChat.Xamarin.MvvmPackage.Services
{
    public class NavigationService : INavigationService
    {
        public Task NavigateToAsync<TPageModel>() where TPageModel : PageModelBase
        {
            return Device.InvokeOnMainThreadAsync(() =>
                Application.Current.MainPage.Navigation.PushAsync(Helpers.CreatePageFromPageModel<TPageModel>())
            );
        }

        public Task PopAsync()
        {
            return Device.InvokeOnMainThreadAsync(() =>
                Application.Current.MainPage.Navigation.PopAsync()
            );
        }
    }
}
