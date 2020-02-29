using Autofac;
using Xamarin.Forms;

namespace RandomChat.Xamarin.MvvmPackage
{
    public static class Helpers
    {
        public static Page CreatePageFromPageModel<TPageModel>() where TPageModel : PageModelBase
        {
            Page page = IoC.Container.ResolveNamed<Page>(typeof(TPageModel).Name.Replace("Model", ""));
            page.BindingContext = IoC.Container.ResolveNamed<PageModelBase>(typeof(TPageModel).Name);
            return page;
        }
    }
}
