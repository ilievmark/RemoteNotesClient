using System;
using System.IO;
using System.Security.Authentication;
using System.Threading.Tasks;
using System.Windows.Input;
using RemoteNotes.Domain.Requests;
using RemoteNotes.Rules.Contract;
using RemoteNotes.Service.Client.Contract.Authorization;
using RemoteNotes.UI.ViewModel.Abstract;
using RemoteNotes.UI.ViewModel.Instrument;
using RemoteNotes.UI.ViewModel.Service;

namespace RemoteNotes.UI.ViewModel
{
    public class LoginPageViewModel : BaseViewModel
    {
        private readonly INavigationController _navigationController;
        private readonly IAuthorizationService _authorizationService;
        private readonly IAuthorizationDataValidator _authorizationDataValidator;

        public LoginPageViewModel(
            INavigationController navigationController,
            IAuthorizationService authorizationService,
            IAuthorizationDataValidator authorizationDataValidator)
        {
            _navigationController = navigationController;
            _authorizationService = authorizationService;
            _authorizationDataValidator = authorizationDataValidator;

            Title = "Authorization";
        }
        
        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
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
        
        public ICommand LoginCommand => new AsyncCommand(CreateAsyncCommandHandler(OnLoginCommand));

        private async Task OnLoginCommand()
        {
            SetLoginErrorMessage(string.Empty);
            
            var login = Login;
            var password = Password;

            try
            {
                ValidateLogin(login);
                ValidatePassword(password);
                await LoginAsync(login, password);
            }
            catch (AuthenticationException authenticationException)
            {
                SetLoginErrorMessage("Authorization error." + Environment.NewLine + authenticationException.Message);
            }
            catch (InvalidDataException invalidDataException)
            {
                SetLoginErrorMessage("Input data not matched the rules." + Environment.NewLine + invalidDataException.Message);
            }
            catch (ArgumentNullException argumentNullException)
            {
                SetLoginErrorMessage("Credentials cant be empty.");
            }
        }

        private void SetLoginErrorMessage(string message)
            => ErrorMessage = message;

        private void ValidateLogin(string login)
            => _authorizationDataValidator.ValidateLogin(login);
        
        private void ValidatePassword(string password)
            => _authorizationDataValidator.ValidatePassword(password);

        private async Task LoginAsync(string login, string password)
        {
            var authModel = new LoginRequest { Login = login, Password = password };
            var result = await _authorizationService.LoginAsync(authModel);

            if (result.TokenModel != null)
                await LoginSuccessAsync();
            else
                await LoginNotSuccessAsync();
        }

        private Task LoginSuccessAsync()
        {
            return _navigationController.NavigateToRootWith(PageKeys.Dashboard);
        }

        private async Task LoginNotSuccessAsync()
        {
            SetLoginErrorMessage("Cant login with given credentials.");
        }
    }
}