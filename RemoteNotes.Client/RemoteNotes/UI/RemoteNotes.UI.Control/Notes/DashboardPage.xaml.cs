using RemoteNotes.Domain.Core.Attributes;
using RemoteNotes.Domain.Core.Constants;
using Xamarin.Forms.Xaml;

namespace RemoteNotes.UI.Control.Notes
{
    [AuthorizedNavigation(AlternativePageTag = PageTagConstants.Login)]
    [PageRegistrattion(NavigationTag = PageTagConstants.Dashboard)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardPage
    {
        public DashboardPage()
        {
            InitializeComponent();
        }
    }
}