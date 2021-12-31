using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using RemoteNotes.Domain.Contract.Navigation;
using RemoteNotes.Domain.Contract.ViewModel;
using RemoteNotes.Domain.Core.Attributes;
using RemoteNotes.Domain.Core.Constants;
using RemoteNotes.Domain.Core.Navigation;
using RemoteNotes.Domain.Models;
using RemoteNotes.Domain.Services.ViewModel;
using RemoteNotes.Service.Client.Contract.Notes;
using RemoteNotes.UI.ViewModel.Tool;

namespace RemoteNotes.UI.ViewModel.Notes
{
    [ViewModelRegistration(NavigationTag = PageTagConstants.Login)]
    public class DashboardPageViewModel : BaseNavigationViewModel, INavigated
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
        
        private ObservableCollection<NoteModel> _items = new ObservableCollection<NoteModel>();
        public ObservableCollection<NoteModel> Items
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
            => Items = new ObservableCollection<NoteModel>(notes);

        public Task OnNavigatedAsync(NavigationData navigationData, CancellationToken token)
        {
            return RefreshItemsAsync();
        }
    }
}