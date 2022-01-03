using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using Plugin.Media.Abstractions;
using RemoteNotes.Domain.Contract.Navigation;
using RemoteNotes.Domain.Entity;
using RemoteNotes.Domain.Models;
using RemoteNotes.Domain.Services.ViewModel;
using RemoteNotes.UI.ViewModel.Tool;
using Xamarin.Forms;

namespace RemoteNotes.UI.ViewModel.Notes
{
    public abstract class NoteEditViewModelBase : BaseNavigationViewModel
    {
        private readonly IMedia _mediaService;
        
        private byte[] _photoBytes;
        
        public NoteEditViewModelBase(
            INavigationService navigationService,
            IUserDialogs userDialogs,
            IMedia mediaService)
            : base(navigationService, userDialogs)
        {
            _mediaService = mediaService;
        }
        
        private NoteViewModel _note;
        public NoteViewModel Note
        {
            get => _note;
            set => SetProperty(ref _note, value);
        }
        
        private ImageSource _photo;
        public ImageSource Photo
        {
            get => _photo;
            set => SetProperty(ref _photo, value);
        }

        public ICommand SaveCommand => new AsyncCommand(() => OnSaveCommandAsync(Note.Note));
        public ICommand CancelCommand => new AsyncCommand(OnCancelCommandAsync);
        public ICommand PickImageCommand => new AsyncCommand(OnPickImageCommandAsync);

        private async Task OnPickImageCommandAsync()
        {
            try
            {
                var pickedImage = await _mediaService.PickPhotoAsync();

                Photo = ImageSource.FromStream(pickedImage.GetStream);

                using (var stream = pickedImage.GetStream())
                {
                    using (BinaryReader br = new BinaryReader(stream))
                    {
                        _photoBytes = br.ReadBytes((int)stream.Length);
                    }
                }
            }
            catch (Exception)
            {
                Debugger.Break();
            }
        }

        private Task OnCancelCommandAsync()
        {
            return NavigationService.NavigateBackAsync(CancellationToken.None);
        }

        protected abstract Task OnSaveAsync(NoteModel note);

        private async Task OnSaveCommandAsync(NoteModel note)
        {
            await OnSaveAsync(note);
            await NavigationService.NavigateBackAsync(CancellationToken.None);
        }
    }
}