using System;
using Autofac;
using RemoteNotes.UI.Shell.Module;
using RemoteNotes.UI.Shell.Navigation;
using Xamarin.Forms;

namespace RemoteNotes.UI.Dependency.Test
{
    public class XamarinApp : Application
    {
        private static XamarinApp Instance { get; set; }

        public static TResolving Resolve<TResolving>() => Instance._container.Resolve<TResolving>();
        public static TResolving Resolve<TResolving>(string key) => Instance._container.ResolveNamed<TResolving>(key);
        
        private IContainer _container;

        public XamarinApp()
        {
            MainPage = new NavigationPage();
            RegisterDependencies();
            Instance = this;
        }
        
        private void RegisterDependencies()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<MainModule>();
            builder.Register(c => new NavigationProvider(() => MainPage.Navigation));
            builder.Register(c => new PageLocator(new Lazy<IContainer>(() => _container)));
            _container = builder.Build();
        }
    }
}