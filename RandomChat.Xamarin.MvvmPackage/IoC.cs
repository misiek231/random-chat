using Autofac;
using Autofac.Core;

namespace RandomChat.Xamarin.MvvmPackage
{
    public static class IoC
    {
        public static IContainer Container;

        public static void BuildContainer(ContainerBuilder builder)
        {
            Container = builder.Build();
        }
    }
}
