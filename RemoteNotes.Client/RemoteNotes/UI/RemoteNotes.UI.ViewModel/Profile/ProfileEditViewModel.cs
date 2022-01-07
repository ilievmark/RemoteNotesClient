using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using RemoteNotes.Domain.Contract.Navigation;
using RemoteNotes.Domain.Contract.ViewModel;
using RemoteNotes.Domain.Core.Attributes;
using RemoteNotes.Domain.Core.Constants;
using RemoteNotes.Domain.Core.Extensions;
using RemoteNotes.Domain.Core.Navigation;
using RemoteNotes.Domain.Models;
using RemoteNotes.Service.Client.Contract.User;
using RemoteNotes.UI.ViewModel.Tool;

namespace RemoteNotes.UI.ViewModel.Profile
{
    [ViewModelRegistration(NavigationTag = PageTags.ProfileEdit)]
    public class ProfileEditViewModel : ProfileViewModelBase, INavigating
    {
        private UserModel _user;
        
        public ProfileEditViewModel(
            IUserService userService,
            INavigationService navigationService,
            IUserDialogs userDialogs)
            : base(userService, navigationService, userDialogs)
        {
            Title = "Edit profile";
        }
        
        public ICommand SaveProfileCommand => new AsyncCommand(() => CommandExecutionHolderMethodAsync(OnSaveProfileAsync));

        private async Task OnSaveProfileAsync()
        {
            UpdateFieldsOfModel(_user);

            using (UserDialogs.Loading())
            {
                await _userService.UpdateUserDataAsync(_user);
            }

            await NavigationService.NavigateBackAsync(CancellationToken.None);
        }

        public Task OnNavigatingAsync(NavigationData navigationData, CancellationToken token)
        {
            _user = navigationData.GetParameter<UserModel>("User");
            LoadFieldsFromModel(_user);
            return Task.CompletedTask;
        }
    }
}