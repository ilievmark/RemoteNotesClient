using RemoteNotes.Domain.Core.Attributes;
using RemoteNotes.Domain.Core.Constants;
using Xamarin.Forms.Xaml;

namespace RemoteNotes.UI.Control.Profile
{
    [AuthorizedNavigation(AlternativePageTag = PageTags.Login)]
    [PageRegistration(NavigationTag = PageTags.ProfileEdit)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfileEditPage
    {
        public ProfileEditPage()
        {
            InitializeComponent();
        }
    }
}