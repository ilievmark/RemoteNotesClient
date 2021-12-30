using System;
using System.IO;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using RemoteNotes.Domain.Contract.Authorization;
using RemoteNotes.Domain.Core.Attributes;
using RemoteNotes.Domain.Core.Constants;
using RemoteNotes.Domain.Extensions;
using RemoteNotes.Domain.Services.Navigation;
using RemoteNotes.Domain.Services.ViewModel;
using RemoteNotes.Service.Client.Contract.Authorization;
using RemoteNotes.UI.ViewModel.Tool;

namespace RemoteNotes.UI.ViewModel.Authorization
{
    [ViewModelRegistration(NavigationTag = PageTagConstants.Login)]
    public class LoginViewModel : BaseNavigationViewModel
    {
        private readonly IAuthorizationHolder _authorizationHolder;
        private readonly IAuthorizationService _authorizationService;
        
        public LoginViewModel(
            NavigationService navigationService,
            IUserDialogs userDialogs,
            IAuthorizationHolder authorizationHolder,
            IAuthorizationService authorizationService)
            : base(navigationService, userDialogs)
        {
            _authorizationHolder = authorizationHolder;
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

            if (_authorizationHolder.GetLastSession().IsValid())
                await NavigationService.NavigateWithReplaceAsync(PageTagConstants.Dashboard, CancellationToken.None);
            else
                await ShowAlertAsync("Cant login with given credentials.");
        }
    }
}