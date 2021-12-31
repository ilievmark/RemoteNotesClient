using Acr.UserDialogs;
using Autofac;
using RemoteNotes.Domain.Contract.Storage;
using RemoteNotes.UI.Shell.Service;

namespace RemoteNotes.UI.Shell.Module
{
    public class CommonToolsModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PreferencesStorage>().As<IPreferencesStorage>().SingleInstance();
            builder.Register(c => UserDialogs.Instance).As<IUserDialogs>().SingleInstance();
        }
    }
}