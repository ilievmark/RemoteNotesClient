using Foundation;
using RemoteNotes.iOS.Delegate;
using RemoteNotes.UI.Shell;
using RemoteNotes.UI.Shell.Application;

namespace RemoteNotes.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : BaseAppDelegate
    {
        protected override void PreInitPackages()
        {
            global::Xamarin.Forms.Forms.Init();
        }

        protected override BaseApplication CreateApplication()
            => new App();
    }
}
