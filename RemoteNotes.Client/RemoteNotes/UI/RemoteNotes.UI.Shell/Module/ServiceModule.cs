using System.Net.Http;
using Autofac;
using RemoteNotes.Service.Client.Contract;
using RemoteNotes.Service.Client.Contract.Authorization;
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
            builder.Register(c => new HttpClient());
            builder.RegisterType<NotesService>().As<INotesService>();
            builder.RegisterType<AuthorizationHolder>().As<IAuthorizationHolder>().As<IAuthorizationUpdater>();

            builder.RegisterType<NotesHub>().As<INotesHub>();
            builder
                .Register(
                    c => new HubSafeGuaranteeConnectionDecorator(
                        c.Resolve<NotesHub>()))
                .Named<IHubConnection>(nameof(NotesHub));

            builder
                .RegisterType<AuthorizationService>()
                .As<IAuthorizationService>();
        }
    }
}