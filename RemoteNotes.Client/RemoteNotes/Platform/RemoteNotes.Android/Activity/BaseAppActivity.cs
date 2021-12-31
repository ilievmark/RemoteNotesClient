using System.Threading.Tasks;
using Android.OS;
using Autofac;
using RemoteNotes.Domain.Contract.Navigation;
using RemoteNotes.UI.Shell.Application;
using Xamarin.Forms.Platform.Android;

namespace RemoteNotes.Android.Activity
{
    public abstract class BaseAppActivity : FormsAppCompatActivity
    {
        protected IContainer Container { get; private set; }

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            await RunAsync(savedInstanceState);
        }

        protected virtual void PreInitPackages(Bundle savedInstanceState)
        {
        }

        protected virtual void PostInitPackages(Bundle savedInstanceState)
        {
        }

        protected abstract BaseApplication CreateApplication();

        protected virtual void SetupNativeDependencies(ContainerBuilder builder)
        {
        }

        private async Task RunAsync(Bundle savedInstanceState)
        {
            PreInitPackages(savedInstanceState);
            var app = CreateApplication();
            var builder = new ContainerBuilder();
            SetupNativeDependencies(builder);
            app.RegisterDependencies(builder);
            app.SetContainer(Container);
            Container = builder.Build();
            await app.SetupNavigationAsync(Container.Resolve<INavigationService>());
            LoadApplication(app);
            PostInitPackages(savedInstanceState);
        }
    }
}
