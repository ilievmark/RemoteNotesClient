using Autofac;
using RemoteNotes.UI.Shell.Navigation;
using RemoteNotes.UI.ViewModel.Service;

namespace RemoteNotes.UI.Shell.Module
{
    public class MainModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<NavigationModule>();

            builder.RegisterType<NavigationController>().As<INavigationController>();
        }
    }
}