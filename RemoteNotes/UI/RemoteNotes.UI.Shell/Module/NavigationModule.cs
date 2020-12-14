using Autofac;
using RemoteNotes.UI.Control;
using RemoteNotes.UI.ViewModel;
using Xamarin.Forms;
using PageKeys = RemoteNotes.UI.ViewModel.PageKeys;

namespace RemoteNotes.UI.Shell.Module
{
    public class NavigationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<LoginPage>().Named<ContentPage>(PageKeys.Login);
            builder.RegisterType<LoginPageViewModel>();
            
            
        }
    }
}