using System;
using System.IO;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using RemoteNotes.Domain.Contract.Navigation;
using RemoteNotes.Domain.Core.Attributes;
using RemoteNotes.Domain.Core.Constants;
using RemoteNotes.Domain.Services.Navigation;
using RemoteNotes.Domain.Services.ViewModel;
using RemoteNotes.Service.Client.Contract.Authorization;
using RemoteNotes.UI.ViewModel.Tool;

namespace RemoteNotes.UI.ViewModel.Authorization
{
    [ViewModelRegistration(NavigationTag = PageTags.Login)]
    public class LoginViewModel : BaseNavigationViewModel
    {
        private readonly IAuthorizationService _authorizationService;
        
        public LoginViewModel(
            INavigationService navigationService,
            IUserDialogs userDialogs,
            IAuthorizationService authorizationService)
            : base(navigationService, userDialogs)
        {
            _authorizationService = authorizationService;

            Title = "Login";
        }
        
        private string _login;
        public string Login
        {
            get => _login;
            set => SetProperty(ref _login, value);
        }
        
        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }
        
        public ICommand LoginCommand => new AsyncCommand(() => CommandExecutionHolderMethodAsync(OnLoginCommand));
        
        public ICommand ToSignUpCommand => new AsyncCommand(() => CommandExecutionHolderMethodAsync(OnToSignUpCommand));

        private Task OnToSignUpCommand()
        {
            return NavigationService.NavigateWithReplaceAsync(PageTags.SignUp, CancellationToken.None);
        }

        private async Task OnLoginCommand()
        {
            var login = Login;
            var password = Password;

            try
            {
                await LoginAsync(login, password);
            }
            catch (AuthenticationException authenticationException)
            {
                ShowToast("Login error. Check, your user auth data is valid.");
            }
            catch (InvalidDataException invalidDataException)
            {
                ShowToast("Input data not matched the rules.");
            }
            catch (ArgumentNullException argumentNullException)
            {
                ShowToast("Credentials cant be empty.");
            }
        }

        private async Task LoginAsync(string login, string password)
        {
            await _authorizationService.SignInAsync(login, password, CancellationToken.None);

            if (_authorizationService.IsAuthorized)
                await NavigationService.NavigateWithReplaceAsync(PageTags.Dashboard, CancellationToken.None);
            else await ShowAlertAsync("Cant login with given credentials.");
        }
    }
}