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

        private readonly ContainerBuilder builder = new ContainerBuilder();
        public static INavigation Navigation;


        public Application()
        {
            RegisterTypes();
        }

        private void RegisterTypes()
        {
            Assembly mscorlib = GetType().Assembly;
            IEnumerable<Type> pageModels = mscorlib.GetTypes().Where(t => t.Name.EndsWith("PageModel")).AsEnumerable();
            IEnumerable<Type> pages = mscorlib.GetTypes().Where(t => t.Name.EndsWith("Page")).AsEnumerable();

            IEnumerable<Type> services = mscorlib.GetTypes().Where(t => t.Name.EndsWith("Service")).AsEnumerable();
            IEnumerable<Type> interfaces = mscorlib.GetTypes().Where(t => t.Name.EndsWith("Service") && t.IsInterface).AsEnumerable();

            foreach (Type service in services)
            {
                Type serviceInterface = interfaces.Where(i => i.Name == "I" + service.Name).FirstOrDefault();
                if (serviceInterface == null || !serviceInterface.IsAssignableFrom(service))
                {
                    continue;
                }

                builder.RegisterType(service).As(serviceInterface);
            }

            builder.RegisterType<NavigationService>().As<INavigationService>();


            foreach (Type pageModel in pageModels)
            {
                if (!pageModel.IsSubclassOf(typeof(PageModelBase)))
                {
                    continue;
                }

                builder.RegisterType(pageModel).Named<PageModelBase>(pageModel.Name);
            }

            foreach (Type page in pages)
            {
                builder.RegisterType(page).Named<Page>(page.Name); ;
            }

            IoC.BuildContainer(builder);
        }

        protected void SetStartPage<T>() where T : PageModelBase
        {
            Page mainPage = IoC.Container.ResolveNamed<Page>(typeof(T).Name.Replace("Model", ""));
            mainPage.BindingContext = IoC.Container.ResolveNamed<PageModelBase>(typeof(T).Name);
            MainPage = new NavigationPage(mainPage);
            Navigation = MainPage.Navigation;
        }
    }
}
