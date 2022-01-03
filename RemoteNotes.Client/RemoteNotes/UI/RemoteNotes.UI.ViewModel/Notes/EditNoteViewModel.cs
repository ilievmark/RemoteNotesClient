using System.Threading;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Plugin.Media.Abstractions;
using RemoteNotes.Domain.Contract.Navigation;
using RemoteNotes.Domain.Contract.ViewModel;
using RemoteNotes.Domain.Core.Attributes;
using RemoteNotes.Domain.Core.Constants;
using RemoteNotes.Domain.Core.Extensions;
using RemoteNotes.Domain.Core.Navigation;
using RemoteNotes.Domain.Models;
using RemoteNotes.Service.Client.Contract.Notes;

namespace RemoteNotes.UI.ViewModel.Notes
{
    [ViewModelRegistration(NavigationTag = PageTags.EditNote)]
    public class EditNoteViewModel : NoteEditViewModelBase, INavigating
    {
        private readonly INotesHub _notesHub;

        public EditNoteViewModel(
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
                await _notesHub.UpdateNoteAsync(note);
            }
        }

        public Task OnNavigatingAsync(NavigationData navigationData, CancellationToken token)
        {
            var note = navigationData.GetParameter<NoteViewModel>("Note");
            Note = note;
            return Task.CompletedTask;
        }
    }
}