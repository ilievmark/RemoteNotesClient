using Acr.UserDialogs;
using Autofac;
using RemoteNotes.Domain.Contract.Navigation;
using RemoteNotes.Domain.Core.Attributes;
using RemoteNotes.Domain.Services.Navigation;
using RemoteNotes.Domain.Services.Navigation.Holders;
using RemoteNotes.Domain.Services.Navigation.Performers;
using RemoteNotes.Domain.Services.NavigationBuilders;
using RemoteNotes.Domain.Services.NavigationBuilders.Registrators;
using RemoteNotes.Domain.Services.ViewModel;
using Xamarin.Forms;

namespace RemoteNotes.UI.Shell.Module
{
    public class NavigationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            RegisterPages(builder);
            RegisterViewModels(builder);

            builder.RegisterType<NavigatedPerformer>().As<INavigationPerformer>();
            builder.RegisterType<NavigatingPerformer>().As<INavigationPerformer>();
            builder.RegisterType<NavigatedFromPerformer>().As<INavigationPerformer>();
            builder.RegisterType<NavigatingFromPerformer>().As<INavigationPerformer>();
            builder.RegisterType<NavigatedBackPerformer>().As<INavigationPerformer>();
            builder.RegisterType<NavigatingBackPerformer>().As<INavigationPerformer>();

            builder.RegisterType<CompositeNavigationPerformer>().As<ICompositeNavigationPerformer>();
            
            builder.RegisterType<PageBuilder>().As<IPageBuilder>();
            builder.RegisterType<ViewModelBuilder>().As<IViewModelBuilder>();
            
            builder.Register(c => UserDialogs.Instance).As<IUserDialogs>().InstancePerLifetimeScope();
            builder.RegisterType<NavigationService>().As<INavigationService>().InstancePerLifetimeScope();
            builder.RegisterDecorator<AuthorizedNavigationServiceDecorator, INavigationService>();

            builder.RegisterType<NavigationPage>().Named<Page>("nav").InstancePerRequest();
        }

        private void RegisterPages(ContainerBuilder builder)
        {
            var typesProvider = new RegisteredNavigationTypesProvider();
            var typeRegistrations = typesProvider.GetRegistrations<PageRegistrationAttribute>("RemoteNotes.UI.Control");

            foreach (var typeRegistration in typeRegistrations)
                builder.RegisterType(typeRegistration.Type).Named<Page>(typeRegistration.Tag).ExternallyOwned();

            builder.Register(c => new PageNavigationTypeHolder(typeRegistrations));
        }

        private void RegisterViewModels(ContainerBuilder builder)
        {
            var typesProvider = new RegisteredNavigationTypesProvider();
            var typeRegistrations = typesProvider.GetRegistrations<ViewModelRegistrationAttribute>("RemoteNotes.UI.ViewModel");

            foreach (var typeRegistration in typeRegistrations)
                builder.RegisterType(typeRegistration.Type).Named<BaseViewModel>(typeRegistration.Tag).ExternallyOwned();

            builder.Register(c => new ViewModelNavigationTypeHolder(typeRegistrations));
        }
    }
}