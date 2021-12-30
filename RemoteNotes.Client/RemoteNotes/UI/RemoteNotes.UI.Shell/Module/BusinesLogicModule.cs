using Autofac;
using RemoteNotes.Rules;
using RemoteNotes.Rules.Contract;

namespace RemoteNotes.UI.Shell.Module
{
    public class BusinesLogicModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AuthorizationDataValidator>().As<IAuthorizationDataValidator>();

        }
    }
}