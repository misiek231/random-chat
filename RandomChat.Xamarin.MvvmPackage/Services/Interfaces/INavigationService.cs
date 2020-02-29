using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RandomChat.Xamarin.MvvmPackage.Services.Interfaces
{
    public interface INavigationService
    {
        Task NavigateToAsync<TPageModel>() where TPageModel : PageModelBase;
    }
}
