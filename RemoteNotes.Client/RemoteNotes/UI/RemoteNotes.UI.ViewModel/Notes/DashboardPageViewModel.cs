using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using RemoteNotes.Domain.Contract.Navigation;
using RemoteNotes.Domain.Contract.ViewModel;
using RemoteNotes.Domain.Core.Attributes;
using RemoteNotes.Domain.Core.Constants;
using RemoteNotes.Domain.Core.Navigation;
using RemoteNotes.Domain.Hubs;
using RemoteNotes.Domain.Models;
using RemoteNotes.Domain.Services.ViewModel;
using RemoteNotes.Service.Client.Contract.Notes;
using RemoteNotes.UI.ViewModel.Tool;

namespace RemoteNotes.UI.ViewModel.Notes
{
    [ViewModelRegistration(NavigationTag = PageTags.Dashboard)]
    public class DashboardPageViewModel : BaseNavigationViewModel, INavigated, IDisposable
    {
        private readonly INotesHub _notesHub;

        public DashboardPageViewModel(
            INavigationService navigationService,
            INotesHub notesHub,
            IUserDialogs userDialogs)
            : base(navigationService, userDialogs)
        {
            _notesHub = notesHub;
            
            Title = "Notes";
        }
        
        private ObservableCollection<NoteViewModel> _items = new ObservableCollection<NoteViewModel>();
        public ObservableCollection<NoteViewModel> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }
        
        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }
        
        public ICommand RefreshCommand => new AsyncCommand(() => CommandExecutionHolderMethodAsync(OnRefreshCommand));
        public ICommand CreateNoteCommand => new AsyncCommand(() => CommandExecutionHolderMethodAsync(OnCreateNoteCommand));
        public ICommand OpenProfileCommand => new AsyncCommand(() => CommandExecutionHolderMethodAsync(OnOpenProfileCommand));
        public ICommand EditNoteCommand => new AsyncCommand(o => CommandExecutionHolderMethodAsync(OnEditNoteCommand, o));
        public ICommand DeleteNoteCommand => new AsyncCommand(o => CommandExecutionHolderMethodAsync(OnDeleteNoteCommand, o));

        private async Task OnDeleteNoteCommand(object arg)
        {
            using (UserDialogs.Loading())
            {
                var note = arg as NoteViewModel;
                await _notesHub.DeleteNoteAsync(note.Note);
            }
        }

        private Task OnEditNoteCommand(object note)
        {
            return NavigationService.NavigateNextAsync(PageTags.EditNote, CancellationToken.None,
                new KeyValuePair<string, object>("Note", note));
        }

        private Task OnOpenProfileCommand()
        {
            return NavigationService.NavigateNextAsync(PageTags.ProfileView, CancellationToken.None);
        }

        private Task OnCreateNoteCommand()
        {
            return NavigationService.NavigateNextAsync(PageTags.CreateNote, CancellationToken.None);
        }

        private Task OnRefreshCommand() => RefreshItemsAsync();

        private async Task RefreshItemsAsync()
        {
            IsRefreshing = true;

            try
            {
                var notes = await GetNotesAsync();
                
                SetItems(notes);
            }
            catch (Exception e)
            {
                Debugger.Break();
            }
            finally
            {
                IsRefreshing = false;
            }
        }

        private Task<IEnumerable<NoteModel>> GetNotesAsync()
            => _notesHub.GetNotesAsync();

        private void SetItems(IEnumerable<NoteModel> notes)
        {
            var bindableNotes = notes.Select(n => new NoteViewModel(n));
            Items = new ObservableCollection<NoteViewModel>(bindableNotes);
        }

        public Task OnNavigatedAsync(NavigationData navigationData, CancellationToken token)
        {
            _notesHub.NoteStorageUpdate += OnNoteUpdated;
            return RefreshItemsAsync();
        }

        public void Dispose()
        {
            _notesHub.NoteStorageUpdate -= OnNoteUpdated;
        }

        private void OnNoteUpdated(NoteChange change, NoteModel note)
        {
            switch (change)
            {
                case NoteChange.Added:
                    Items.Add(new NoteViewModel(note));
                    break;
                case NoteChange.Deleted:
                    var noteToRemove = Items.FirstOrDefault(n => n.Id == note.Id);
                    Items.Remove(noteToRemove);
                    break;
                case NoteChange.Updated:
                    var noteToUpdate = Items.FirstOrDefault(n => n.Id == note.Id);
                    noteToUpdate.Note = note;
                    break;
                default:
                    throw new DataException("Invalid note with id: " + note.Id);
            }
        }
    }
}