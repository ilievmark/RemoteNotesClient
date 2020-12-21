using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using RemoteNotes.Service.Client.Contract;
using RemoteNotes.Service.Client.Contract.Model;
using RemoteNotes.UI.ViewModel.Abstract;
using RemoteNotes.UI.ViewModel.Instrument;
using RemoteNotes.UI.ViewModel.Model;

namespace RemoteNotes.UI.ViewModel
{
    public class DashboardPageViewModel : BaseViewModel, IAppearable
    {
        private readonly INotesService _notesService;

        public DashboardPageViewModel(INotesService notesService)
        {
            _notesService = notesService;
            
            Title = "Dashboard";
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
        
        public ICommand RefreshCommand => new AsyncCommand(CreateAsyncCommandHandler(OnRefreshCommand));

        private Task OnRefreshCommand() => RefreshItemsAsync();

        public async void Appeared() => await RefreshItemsAsync();

        private async Task RefreshItemsAsync()
        {
            IsRefreshing = true;

            try
            {
                var notes = await GetNotesAsync();
                var vmNotes = ConvertNotes(notes);
                
                SetItems(vmNotes);
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
            => _notesService.GetNotesAsync();

        private IEnumerable<NoteViewModel> ConvertNotes(IEnumerable<NoteModel> notes)
            => notes.Select(n => NoteViewModel.From(n));

        private void SetItems(IEnumerable<NoteViewModel> notes)
            => Items = new ObservableCollection<NoteViewModel>(notes);
    }
}