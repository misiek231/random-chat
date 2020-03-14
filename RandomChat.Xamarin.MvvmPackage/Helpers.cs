using Autofac;
using Xamarin.Forms;

namespace RandomChat.Xamarin.MvvmPackage
{
    public static class Helpers
    {
        public static Page CreatePageFromPageModel<TPageModel>() where TPageModel : PageModelBase
        {
            Page page = IoC.Container.ResolveNamed<Page>(typeof(TPageModel).Name.Replace("Model", ""));
            PageModelBase pageModel = IoC.Container.ResolveNamed<PageModelBase>(typeof(TPageModel).Name);
            page.BindingContext = pageModel;
            NavigationPage n = (NavigationPage)Application.Current.MainPage;
            if (n != null)
            {
                n.Popped += (s, e) => { if (e.Page == page) { pageModel.OnPopped(); } };
            }

            page.Disappearing += async (s, e) => await pageModel.OnDisappearingAsync().ConfigureAwait(false);
            page.Appearing += async (s, e) => await pageModel.OnAppearingAsync().ConfigureAwait(false);
            return page;
        }
    }
}
