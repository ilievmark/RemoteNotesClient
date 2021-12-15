using System.Net.Http;
using Autofac;
using RemoteNotes.API;
using RemoteNotes.API.Contract;

namespace RemoteNotes.UI.Shell.Module
{
    public class ApiBindingsModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new HttpClient()).InstancePerLifetimeScope();
            
            builder.RegisterType<AuthorizationApi>().As<IAuthorizationApi>().InstancePerLifetimeScope();
        }
    }
}