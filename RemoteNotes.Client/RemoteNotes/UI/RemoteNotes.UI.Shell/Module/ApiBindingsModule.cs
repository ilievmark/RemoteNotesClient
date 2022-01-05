using System.Net.Http;
using Autofac;
using RemoteNotes.API.Contract;

#if MOCK
using RemoteNotes.API.Stub;
#else
using RemoteNotes.API;
#endif

namespace RemoteNotes.UI.Shell.Module
{
    public class ApiBindingsModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new HttpClient()).InstancePerLifetimeScope();

            builder.RegisterType<AuthorizationApi>().As<IAuthorizationApi>().InstancePerLifetimeScope();
            builder.RegisterType<UserApi>().As<IUserApi>().InstancePerLifetimeScope();
        }
    }
}