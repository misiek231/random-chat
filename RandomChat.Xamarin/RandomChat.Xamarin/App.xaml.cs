using RandomChat.Xamarin.MvvmPackage;
using RandomChat.Xamarin.PageModels;
using RandomChat.Xamarin.Pages;
using RandomChat.Xamarin.Services;

namespace RandomChat.Xamarin
{
    public partial class App : Application
    {

        public App() : base()
        {
            InitializeComponent();
            SetStartPage<MainPageModel>();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
