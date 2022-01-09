using Autofac;
using Autofac.Core;
using RemoteNotes.Service.Client.Contract.Hub;
using RemoteNotes.Service.Client.Contract.Notes;
using RemoteNotes.Service.Client.Contract.User;
using RemoteNotes.Service.Domain;
using RemoteNotes.Service.Domain.Hub;

#if MOCK
using RemoteNotes.Service.Domain.Stub.Notes;
using RemoteNotes.Service.Domain.Stub.User;
#else
using RemoteNotes.Service.Domain.Notes;
using RemoteNotes.Service.Domain.User;
#endif

namespace RemoteNotes.UI.Shell.Module
{
    public class HubModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<HubConfiguration>().SingleInstance();
            builder.RegisterType<HubReconnector>().Named<IHubReconnector>(Hubs.Notes).SingleInstance();
            builder.RegisterType<Hub>().SingleInstance()
                   .WithParameter(TypedParameter.From(Hubs.Notes))
                   .Named<IHubConnection>(Hubs.Notes)
                   .Named<IHubObservable>(Hubs.Notes)
                   .Named<IHubMessager>(Hubs.Notes);
            
            builder.RegisterType<HubController>().Named<IHubController>(Hubs.Notes).SingleInstance()
                   .WithParameter(ResolvedParameter.ForNamed<IHubReconnector>(Hubs.Notes))
                   .WithParameter(ResolvedParameter.ForNamed<IHubConnection>(Hubs.Notes))
                   .WithParameter(ResolvedParameter.ForNamed<IHubObservable>(Hubs.Notes))
                   .WithParameter(TypedParameter.From(Hubs.Notes));
            
            builder.RegisterType<HubReconnector>().Named<IHubReconnector>(Hubs.User).SingleInstance();
            builder.RegisterType<Hub>().SingleInstance()
                   .WithParameter(TypedParameter.From(Hubs.User))
                   .Named<IHubConnection>(Hubs.User)
                   .Named<IHubObservable>(Hubs.User)
                   .Named<IHubMessager>(Hubs.User);
            
            builder.RegisterType<HubController>().Named<IHubController>(Hubs.User).SingleInstance()
                   .WithParameter(ResolvedParameter.ForNamed<IHubReconnector>(Hubs.User))
                   .WithParameter(ResolvedParameter.ForNamed<IHubConnection>(Hubs.User))
                   .WithParameter(ResolvedParameter.ForNamed<IHubObservable>(Hubs.User))
                   .WithParameter(TypedParameter.From(Hubs.User));

            builder.Register(
                    c => new HubCompositeController(
                        c.ResolveNamed<IHubController>(Hubs.Notes),
                        c.ResolveNamed<IHubController>(Hubs.User)))
                   .As<IHubController>()
                   .SingleInstance();
            
#if MOCK
            builder.Register(
                    c => new HubCompositeController())
                .As<IHubController>()
                .SingleInstance();
#endif
            
            builder.RegisterType<NotesHub>().SingleInstance()
                   .As<INotesHub>().As<IHubObserver>()
                   .WithParameter(ResolvedParameter.ForNamed<IHubMessager>(Hubs.Notes));

            builder.RegisterType<UserHub>().SingleInstance()
                   .As<IUserHub>().As<IHubObserver>();
            
            builder.RegisterType<UserService>().As<IUserService>().SingleInstance();
        }
    }
}