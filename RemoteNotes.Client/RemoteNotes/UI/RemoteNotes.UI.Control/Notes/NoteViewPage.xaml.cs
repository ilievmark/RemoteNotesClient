using RemoteNotes.Domain.Core.Attributes;
using RemoteNotes.Domain.Core.Constants;
using Xamarin.Forms.Xaml;

namespace RemoteNotes.UI.Control.Notes
{
    [AuthorizedNavigation(AlternativePageTag = PageTags.Login)]
    [PageRegistration(NavigationTag = PageTags.ViewNote)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NoteViewPage
    {
        public NoteViewPage()
        {
            InitializeComponent();
        }
    }
}