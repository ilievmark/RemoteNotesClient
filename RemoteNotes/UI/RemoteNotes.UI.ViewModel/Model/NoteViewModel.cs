using RemoteNotes.Service.Client.Contract.Model;

namespace RemoteNotes.UI.ViewModel.Model
{
    public class NoteViewModel : INote
    {
        private NoteViewModel() {}

        public static NoteViewModel From(INote note)
            => new NoteViewModel();
    }
}