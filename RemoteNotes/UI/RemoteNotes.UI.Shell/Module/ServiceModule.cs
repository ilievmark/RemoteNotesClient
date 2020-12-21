using Autofac;
using Autofac.Core;
using RemoteNotes.Service.Client.Contract;
using RemoteNotes.Service.Client.Contract.Hub;
using RemoteNotes.Service.Client.Stub;

//using RemoteNotes.Service.Domain.Hub;
using RemoteNotes.Service.Client.Stub.Hub;
using RemoteNotes.UI.Shell.Service;

namespace RemoteNotes.UI.Shell.Module
{
    public class ServiceModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<NotesService>().As<INotesService>();
            builder.RegisterType<NotesHub>().As<INotesHub>();
            builder.RegisterType<AuthorizationHolder>().As<IAuthorizationHolder>().As<IAuthorizationUpdater>();
            
            builder
                .RegisterType<AuthorizationHub>()
                .AsSelf()
                .As<IAuthorizationHub>();

            builder
                .Register(
                    c => new HubSafeGuaranteeConnectionDecorator(
                        c.Resolve<AuthorizationHub>()))
                .Named<IHubConnection>(nameof(AuthorizationHub));

            builder
                .RegisterType<AuthorizationService>()
                .WithParameter(ResolvedParameter.ForNamed<IHubConnection>(nameof(AuthorizationHub)))
                .As<IAuthorizationService>();
        }
    }
}