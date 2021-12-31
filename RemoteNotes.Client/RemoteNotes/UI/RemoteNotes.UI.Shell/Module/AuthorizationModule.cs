using Autofac;
using RemoteNotes.Domain.Contract.Authorization;
using RemoteNotes.Domain.Services.Authorization;
using RemoteNotes.Rules;
using RemoteNotes.Rules.Contract;
using RemoteNotes.Service.Client.Contract.Authorization;
using RemoteNotes.Service.Client.Contract.Hub;

using RemoteNotes.Service.Domain.Authorization;
//using RemoteNotes.Service.Client.Stub;

namespace RemoteNotes.UI.Shell.Module
{
    public class AuthorizationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AuthorizationDataValidator>().As<IAuthorizationDataValidator>();
            builder.RegisterType<AuthorizationHolder>().InstancePerLifetimeScope()
                   .As<IAuthorizationHolder>().As<IAuthorizationUpdater>().As<IHubAuthorizationProvider>();
            builder.RegisterType<AuthorizationService>().As<IAuthorizationService>().InstancePerLifetimeScope();
            //builder.RegisterType<AuthorizationService_stub>().As<IAuthorizationService>().InstancePerLifetimeScope();
        }
    }
}