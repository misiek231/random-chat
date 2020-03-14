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
            page.Disappearing += async (s, e) => await pageModel.OnDisappearingAsync().ConfigureAwait(false);
            page.Appearing += async (s, e) => await pageModel.OnAppearingAsync().ConfigureAwait(false);
            return page;
        }
    }
}
