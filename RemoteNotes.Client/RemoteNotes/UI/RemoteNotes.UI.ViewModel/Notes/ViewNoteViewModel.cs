using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using Plugin.Media.Abstractions;
using RemoteNotes.Domain.Contract.Navigation;
using RemoteNotes.Domain.Contract.ViewModel;
using RemoteNotes.Domain.Core.Attributes;
using RemoteNotes.Domain.Core.Constants;
using RemoteNotes.Domain.Core.Extensions;
using RemoteNotes.Domain.Core.Navigation;
using RemoteNotes.Domain.Models;
using RemoteNotes.UI.ViewModel.Tool;

namespace RemoteNotes.UI.ViewModel.Notes
{
    [ViewModelRegistration(NavigationTag = PageTags.ViewNote)]
    public class ViewNoteViewModel : NoteEditViewModelBase, INavigating
    {
        public ViewNoteViewModel(
            INavigationService navigationService,
            IUserDialogs userDialogs,
            IMedia mediaService)
            : base(navigationService, userDialogs, mediaService)
        {
        }
        
        public ICommand EditCommand => new AsyncCommand(() => OnEditCommandAsync(Note));

        protected override Task OnSaveAsync(NoteModel note)
        {
            return Task.CompletedTask;
        }

        public Task OnNavigatingAsync(NavigationData navigationData, CancellationToken token)
        {
            var note = navigationData.GetParameter<NoteViewModel>("Note");
            Note = note;
            return Task.CompletedTask;
        }

        private Task OnEditCommandAsync(NoteViewModel noteNote)
        {
            return NavigationService.NavigateNextAsync(PageTags.EditNote, CancellationToken.None,
                new KeyValuePair<string, object>("Note", Note));
        }
    }
}