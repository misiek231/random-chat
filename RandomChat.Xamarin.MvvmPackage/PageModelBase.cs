using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RandomChat.Xamarin.MvvmPackage
{
    public abstract class PageModelBase
    {
        public virtual Task OnDisappearingAsync()
        {
            return Task.CompletedTask;
        }

        public virtual Task OnAppearingAsync()
        {
            return Task.CompletedTask;
        }

        public virtual Task OnPopped()
        {
            return Task.CompletedTask;
        }
    }
}
