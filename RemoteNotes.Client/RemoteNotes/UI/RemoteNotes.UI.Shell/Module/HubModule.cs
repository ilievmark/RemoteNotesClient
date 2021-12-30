using Autofac;
using Autofac.Core;
using RemoteNotes.Service.Client.Contract.Hub;
using RemoteNotes.Service.Client.Contract.Notes;
using RemoteNotes.Service.Domain;
using RemoteNotes.Service.Domain.Hub;
using RemoteNotes.Service.Domain.Notes;

namespace RemoteNotes.UI.Shell.Module
{
    public class HubModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<HubConfiguration>().SingleInstance();
            builder.RegisterType<HubReconnector>().Named<IHubReconnector>(Hubs.Notes).SingleInstance();
            builder.RegisterType<Hub>().SingleInstance()
                   .Named<IHubConnection>(Hubs.Notes)
                   .Named<IHubObservable>(Hubs.Notes)
                   .Named<IHubMessager>(Hubs.Notes);
            
            builder.RegisterType<HubController>().Named<IHubController>(Hubs.Notes).SingleInstance()
                   .WithParameter(ResolvedParameter.ForNamed<IHubReconnector>(Hubs.Notes))
                   .WithParameter(ResolvedParameter.ForNamed<IHubConnection>(Hubs.Notes))
                   .WithParameter(ResolvedParameter.ForNamed<IHubObservable>(Hubs.Notes));

            builder.Register(
                    c => new HubCompositeController(
                        c.ResolveNamed<IHubController>(Hubs.Notes)))
                   .As<IHubController>()
                   .SingleInstance();
            
            builder.RegisterType<NotesHub>().SingleInstance()
                   .As<INotesHub>().As<IHubObserver>()
                   .WithParameter(ResolvedParameter.ForNamed<IHubMessager>(Hubs.Notes));
        }
    }
}