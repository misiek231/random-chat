using RandomChat.Xamarin.MvvmPackage.Services.Interfaces;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RandomChat.Xamarin.MvvmPackage.Services
{
    public class NavigationService : INavigationService
    {
        public async Task NavigateToAsync<TPageModel>() where TPageModel : PageModelBase
        {
            Page i = Helpers.CreatePageFromPageModel<TPageModel>();
            await Application.Navigation.PushAsync(i);
        }
    }
}
