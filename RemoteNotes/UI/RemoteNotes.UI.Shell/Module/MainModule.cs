using Autofac;

namespace RemoteNotes.UI.Shell.Module
{
    public class MainModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<NavigationModule>();
            builder.RegisterModule<ServiceModule>();
            builder.RegisterModule<RulesModule>();
        }
    }
}