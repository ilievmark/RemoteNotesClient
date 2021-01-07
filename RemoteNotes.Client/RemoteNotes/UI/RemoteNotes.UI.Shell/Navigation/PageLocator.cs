using System;
using Autofac;
using Xamarin.Forms;

namespace RemoteNotes.UI.Shell.Navigation
{
    public class PageLocator
    {
        private readonly Lazy<IContainer> _container;

        public PageLocator(Lazy<IContainer> container)
        {
            _container = container;
        }

        public ContentPage ResolveNamedPage(string pageName)
            => _container.Value.ResolveNamed<ContentPage>(pageName);
    }
}