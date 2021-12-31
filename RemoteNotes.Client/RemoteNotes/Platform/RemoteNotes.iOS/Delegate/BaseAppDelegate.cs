using Autofac;
using Foundation;
using RemoteNotes.Domain.Contract.Container;
using RemoteNotes.Domain.Contract.Navigation;
using RemoteNotes.Domain.Services.Container;
using RemoteNotes.UI.Shell.Application;
using UIKit;
using Xamarin.Forms;

namespace RemoteNotes.iOS.Delegate
{
    public abstract class BaseAppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        protected IContainer Container { get; private set; }

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            PreInitPackages();
            var application = CreateApplication();
            var builder = new ContainerBuilder();
            SetupNativeDependencies(builder);
            application.RegisterDependencies(builder);
            Container = builder.Build();
            application.SetContainer(Container);
            LoadApplication(application);
            Device.BeginInvokeOnMainThread(async () => await application.SetupNavigationAsync(Container.Resolve<INavigationService>()));
            PostInitPackages();

            return base.FinishedLaunching(app, options);
        }

        protected virtual void PreInitPackages()
        {
        }

        protected virtual void PostInitPackages()
        {
        }

        protected abstract BaseApplication CreateApplication();

        protected virtual void SetupNativeDependencies(ContainerBuilder builder)
        {
            builder.Register(c => new TypeResolver(() => Container)).As<ITypeResolver>();
        }
    }
}
