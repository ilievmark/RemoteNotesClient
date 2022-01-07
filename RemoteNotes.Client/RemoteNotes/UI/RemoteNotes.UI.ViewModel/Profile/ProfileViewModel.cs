using System.Collections.Generic;
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
using RemoteNotes.Service.Client.Contract.User;
using RemoteNotes.UI.ViewModel.Tool;

namespace RemoteNotes.UI.ViewModel.Profile
{
    [ViewModelRegistration(NavigationTag = PageTags.ProfileView)]
    public class ProfileViewModel : ProfileViewModelBase, INavigated
    {
        private UserModel _user;

        public ProfileViewModel(
            IUserService userService,
            INavigationService navigationService,
            IUserDialogs userDialogs)
            : base(userService, navigationService, userDialogs)
        {
            Title = "Profile";
        }
        
        public ICommand EditProfileCommand => new AsyncCommand(() => CommandExecutionHolderMethodAsync(OnEditProfileAsync));

        private Task OnEditProfileAsync()
        {
            return NavigationService.NavigateNextAsync(PageTags.ProfileEdit, CancellationToken.None,
                new KeyValuePair<string, object>("User", _user));
        }

        public async Task OnNavigatedAsync(NavigationData navigationData, CancellationToken token)
        {
            using (UserDialogs.Loading())
            {
                _user = await _userService.GetUserDataAsync();
                LoadFieldsFromModel(_user);
            }
        }

    }
}