using Acr.UserDialogs;
using Autofac;
using Plugin.Media;
using Plugin.Media.Abstractions;
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
            builder.Register(c => CrossMedia.Current).As<IMedia>().SingleInstance();
        }
    }
}