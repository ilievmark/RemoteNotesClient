using Acr.UserDialogs;
using RemoteNotes.Domain.Contract.Navigation;
using RemoteNotes.Domain.Models;
using RemoteNotes.Domain.Services.ViewModel;
using RemoteNotes.Service.Client.Contract.User;

namespace RemoteNotes.UI.ViewModel.Profile
{
    public abstract class ProfileViewModelBase : BaseNavigationViewModel
    {
        protected readonly IUserService _userService;

        public ProfileViewModelBase(
            IUserService userService,
            INavigationService navigationService,
            IUserDialogs userDialogs)
            : base(navigationService, userDialogs)
        {
            _userService = userService;
        }

        private string _userName;
        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value);
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
        
        private string _surname;
        public string Surname
        {
            get => _surname;
            set => SetProperty(ref _surname, value);
        }
        
        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }
        
        private string _photoUrl;
        public string PhotoUrl
        {
            get => _photoUrl;
            set => SetProperty(ref _photoUrl, value);
        }

        protected void LoadFieldsFromModel(UserModel user)
        {
            Name = user.Name;
            Surname = user.Surname;
            Email = user.Email;
            PhotoUrl = user.PhotoUrl;
            UserName = user.UserName;
        }

        protected void UpdateFieldsOfModel(UserModel user)
        {
            user.Name = Name;
            user.Surname = Surname;
            user.Email = Email;
            user.PhotoUrl = PhotoUrl;
        }
    }
}