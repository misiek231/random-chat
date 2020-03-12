using Autofac;
using RandomChat.Xamarin.MvvmPackage.Services;
using RandomChat.Xamarin.MvvmPackage.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;
using Forms = Xamarin.Forms;


namespace RandomChat.Xamarin.MvvmPackage
{
    public abstract class Application : Forms.Application
    {

        public Application()
        {
            IoC.RegisterTypes(GetType().Assembly);
        }

        protected void SetStartPage<TPageModel>() where TPageModel : PageModelBase
        {
            MainPage = new NavigationPage(Helpers.CreatePageFromPageModel<TPageModel>());
        }
    }
}
