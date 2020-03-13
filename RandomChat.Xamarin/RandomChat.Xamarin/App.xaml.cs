using RandomChat.Xamarin.MvvmPackage;
using RandomChat.Xamarin.PageModels;

namespace RandomChat.Xamarin
{
    public partial class App : Application
    {

        public App()
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
