using RemoteNotes.Service.Client.Contract.Model;
using RemoteNotes.UI.ViewModel.Abstract;

namespace RemoteNotes.UI.ViewModel.Model
{
    public class NoteViewModel : BindableBase, INote
    {
        private NoteViewModel() {}

        private int _id;
        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private string _description;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }
        
        public static NoteViewModel From(INote note)
            => new NoteViewModel
            {
                Id = note.Id,
                Title = note.Title,
                Description = note.Description
            };
    }
}