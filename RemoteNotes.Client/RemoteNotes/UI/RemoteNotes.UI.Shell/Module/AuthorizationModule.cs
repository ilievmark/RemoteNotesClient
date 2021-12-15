using Autofac;
using RemoteNotes.Domain.Contract.Authorization;
using RemoteNotes.Domain.Services.Authorization;
using RemoteNotes.Service.Client.Contract.Authorization;

namespace RemoteNotes.UI.Shell.Module
{
    public class AuthorizationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AuthorizationHolder>().As<IAuthorizationHolder>().InstancePerLifetimeScope();

            //builder.RegisterType<AuthorizationService>().As<IAuthorizationService>().InstancePerLifetimeScope();
            //builder.RegisterType<AuthorizationService_stub>().As<IAuthorizationService>().InstancePerLifetimeScope();
        }
    }
}