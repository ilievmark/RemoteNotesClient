using System.Threading;
using System.Threading.Tasks;
using Autofac;
using RemoteNotes.Domain.Contract.Navigation;
using RemoteNotes.Domain.Core.Constants;
using RemoteNotes.Domain.Services.RootCasts;
using RemoteNotes.Service.Client.Contract.Hub;
using RemoteNotes.UI.Shell.Linking;
using RemoteNotes.UI.Shell.Module;
using Xamarin.Forms;

namespace RemoteNotes.UI.Shell
{
    public partial class App
    {
        private IHubController _hubController;
        private CancellationTokenSource _appCancellationTokenSource;
        
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new ContentPage());
        }

        protected override async void OnStart()
        {
            _appCancellationTokenSource = new CancellationTokenSource();

            new ViewModelAssemblyIncluder().LinkAssembly();
            new ViewAssemblyIncluder().LinkAssembly();

            _hubController = Container.Resolve<IHubController>();
            await _hubController.StartAsync();
        }

        protected override async void OnSleep()
        {
            _appCancellationTokenSource.Cancel();
            await _hubController.StopAsync();
        }

        protected override async void OnResume()
        {
            _appCancellationTokenSource = new CancellationTokenSource();
            await _hubController.StartAsync();
        }

        public override void RegisterDependencies(ContainerBuilder builder)
        {
            base.RegisterDependencies(builder);

            builder.RegisterModule<MainModule>();
            
            builder.Register(c => new NavigationProvider(() => MainPage.Navigation));
        }

        public override Task SetupNavigationAsync(INavigationService navigationService)
        {
            return navigationService.NavigateWithReplaceAsync(PageTags.Dashboard, _appCancellationTokenSource.Token);
        }
    }
}
