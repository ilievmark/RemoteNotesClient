using RemoteNotes.Domain.Core.Attributes;
using RemoteNotes.Domain.Core.Constants;
using Xamarin.Forms.Xaml;

namespace RemoteNotes.UI.Control.Notes
{
    [AuthorizedNavigation(AlternativePageTag = PageTags.Login)]
    [PageRegistration(NavigationTag = PageTags.CreateNote)]
    [PageRegistration(NavigationTag = PageTags.EditNote)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NoteEditorPage
    {
        public NoteEditorPage()
        {
            InitializeComponent();
        }
    }
}