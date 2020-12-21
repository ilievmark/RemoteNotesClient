using Autofac;
using RemoteNotes.UI.Control;
using RemoteNotes.UI.Shell.Navigation;
using RemoteNotes.UI.ViewModel;
using RemoteNotes.UI.ViewModel.Service;
using Xamarin.Forms;
using PageKeys = RemoteNotes.UI.ViewModel.PageKeys;

namespace RemoteNotes.UI.Shell.Module
{
    public class NavigationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<NavigationController>().As<INavigationController>();
            
            builder.RegisterType<LoginPage>().Named<ContentPage>(PageKeys.Login);
            builder.RegisterType<LoginPageViewModel>();
            
            
        }
    }
}