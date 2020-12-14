using System;
using Xamarin.Forms;

namespace RemoteNotes.UI.Shell.Navigation
{
    public class NavigationProvider
    {
        private readonly Func<INavigation> _func;

        public NavigationProvider(Func<INavigation> func)
        {
            _func = func;
        }

        public INavigation GetNavigation() => _func();
    }
}