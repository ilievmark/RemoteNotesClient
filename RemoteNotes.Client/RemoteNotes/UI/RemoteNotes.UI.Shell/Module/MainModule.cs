using Autofac;

namespace RemoteNotes.UI.Shell.Module
{
    public class MainModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<CommonToolsModule>();
            builder.RegisterModule<ApiBindingsModule>();
            builder.RegisterModule<HubModule>();
            builder.RegisterModule<AuthorizationModule>();
            builder.RegisterModule<BusinesLogicModule>();
            builder.RegisterModule<NavigationModule>();
        }
    }
}