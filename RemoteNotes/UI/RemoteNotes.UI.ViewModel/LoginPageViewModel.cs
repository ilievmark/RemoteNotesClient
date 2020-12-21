using System;
using System.Diagnostics;
using System.IO;
using System.Security.Authentication;
using System.Threading.Tasks;
using System.Windows.Input;
using RemoteNotes.Rules.Contract;
using RemoteNotes.Service.Client.Contract;
using RemoteNotes.Service.Client.Contract.Model;
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
                Debugger.Break();
            }
            catch (InvalidDataException invalidDataException)
            {
                Debugger.Break();
            }
            catch (ArgumentNullException argumentNullException)
            {
                Debugger.Break();
            }
        }

        private void ValidateLogin(string login)
            => _authorizationDataValidator.ValidateLogin(login);
        
        private void ValidatePassword(string password)
            => _authorizationDataValidator.ValidatePassword(password);

        private async Task LoginAsync(string login, string password)
        {
            var authModel = AuthModel.From(login, password);
            var result = await _authorizationService.LoginAsync(authModel);

            if (result.Success)
                await LoginSuccessAsync(result);
            else
                await LoginNotSuccessAsync(result);
        }

        private Task LoginSuccessAsync(AuthResult result)
        {
            return _navigationController.NavigateToAsync(PageKeys.Dashboard);
        }

        private async Task LoginNotSuccessAsync(AuthResult result)
        {

        }
    }
}