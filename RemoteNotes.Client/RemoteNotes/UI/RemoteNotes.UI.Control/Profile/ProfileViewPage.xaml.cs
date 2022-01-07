using RemoteNotes.Domain.Core.Attributes;
using RemoteNotes.Domain.Core.Constants;
using Xamarin.Forms.Xaml;

namespace RemoteNotes.UI.Control.Profile
{
    [AuthorizedNavigation(AlternativePageTag = PageTags.Login)]
    [PageRegistration(NavigationTag = PageTags.ProfileView)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfileViewPage
    {
        public ProfileViewPage()
        {
            InitializeComponent();
        }
    }
}