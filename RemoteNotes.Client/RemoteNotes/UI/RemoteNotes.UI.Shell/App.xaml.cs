using System;
using Autofac;
using RemoteNotes.Service.Client.Contract;
using RemoteNotes.UI.Shell.Module;
using RemoteNotes.UI.Shell.Navigation;
using RemoteNotes.UI.ViewModel;
using RemoteNotes.UI.ViewModel.Service;
using Xamarin.Forms;

namespace RemoteNotes.UI.Shell
{
    public partial class App
    {
        private IContainer Container;
        
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage();
            RegisterDependencies();
            SetupNavigation();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private void RegisterDependencies()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<MainModule>();
            builder.Register(c => new NavigationProvider(() => MainPage.Navigation));
            builder.Register(c => new PageLocator(new Lazy<IContainer>(() => Container)));
            Container = builder.Build();
        }

        private async void SetupNavigation()
        {
            var authHolder = Container.Resolve<IAuthorizationHolder>();
            var navController = Container.Resolve<INavigationController>();
            
            if (authHolder.IsAuthorized)
                await navController.NavigateToAsync(PageKeys.Dashboard);
            else
                await navController.NavigateToAsync(PageKeys.Login);
        }
    }
}
