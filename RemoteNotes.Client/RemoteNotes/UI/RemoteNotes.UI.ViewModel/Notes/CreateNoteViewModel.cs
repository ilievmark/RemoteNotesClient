using System.Threading;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Plugin.Media.Abstractions;
using RemoteNotes.Domain.Contract.Navigation;
using RemoteNotes.Domain.Contract.ViewModel;
using RemoteNotes.Domain.Core.Attributes;
using RemoteNotes.Domain.Core.Constants;
using RemoteNotes.Domain.Core.Navigation;
using RemoteNotes.Domain.Models;
using RemoteNotes.Service.Client.Contract.Notes;

namespace RemoteNotes.UI.ViewModel.Notes
{
    [ViewModelRegistration(NavigationTag = PageTags.CreateNote)]
    public class CreateNoteViewModel : NoteEditViewModelBase, INavigating
    {
        private readonly INotesHub _notesHub;

        public CreateNoteViewModel(
            INavigationService navigationService,
            IUserDialogs userDialogs,
            INotesHub notesHub,
            IMedia mediaService)
            : base(navigationService, userDialogs, mediaService)
        {
            _notesHub = notesHub;
        }

        protected override async Task OnSaveAsync(NoteModel note)
        {
            using (UserDialogs.Loading())
            {
                await _notesHub.PutNoteAsync(note);
            }
        }

        public Task OnNavigatingAsync(NavigationData navigationData, CancellationToken token)
        {
            Note = new NoteViewModel(new NoteModel());
            return Task.CompletedTask;
        }
    }
}