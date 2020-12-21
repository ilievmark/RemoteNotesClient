using Autofac;
using RemoteNotes.Rules;
using RemoteNotes.Rules.Contract;

namespace RemoteNotes.UI.Shell.Module
{
    public class RulesModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AuthorizationDataValidator>().As<IAuthorizationDataValidator>();
        }
    }
}