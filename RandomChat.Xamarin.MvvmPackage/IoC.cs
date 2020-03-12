using Autofac;
using Autofac.Core;
using RandomChat.Xamarin.MvvmPackage.Attributes;
using RandomChat.Xamarin.MvvmPackage.Services;
using RandomChat.Xamarin.MvvmPackage.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;

namespace RandomChat.Xamarin.MvvmPackage
{
    public static class IoC
    {
        public static IContainer Container;
        private static readonly ContainerBuilder builder = new ContainerBuilder();

        public static void RegisterTypes(Assembly coreProjectAssemby)
        {
            builder.RegisterAssemblyTypes(coreProjectAssemby)
                .Where(t => t.GetCustomAttribute<SingleInstanceAttribute>() == null)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(coreProjectAssemby)
                .Where(t => t.GetCustomAttribute<SingleInstanceAttribute>() != null)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<NavigationService>().As<INavigationService>();

            builder.RegisterAssemblyTypes(coreProjectAssemby)
                .Where(t => t.IsSubclassOf(typeof(PageModelBase)))
                .Where(t => t.Name.EndsWith("PageModel"))
                .Named<PageModelBase>(t => t.Name);

            builder.RegisterAssemblyTypes(coreProjectAssemby)
                .Where(t => t.IsSubclassOf(typeof(Page)))
                .Where(t => t.Name.EndsWith("Page"))
                .Named<Page>(t => t.Name);

            Container = builder.Build();
        }
    }
}
