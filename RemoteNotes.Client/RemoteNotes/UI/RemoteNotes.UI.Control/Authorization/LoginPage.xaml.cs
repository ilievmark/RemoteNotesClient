using RemoteNotes.Domain.Core.Attributes;
using RemoteNotes.Domain.Core.Constants;
using Xamarin.Forms.Xaml;

namespace RemoteNotes.UI.Control.Authorization
{
    [PageRegistration(NavigationTag = PageTagConstants.Login)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }
    }
}