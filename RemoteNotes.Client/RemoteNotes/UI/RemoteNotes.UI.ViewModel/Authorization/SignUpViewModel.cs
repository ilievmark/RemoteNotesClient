using System;
using System.IO;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using RemoteNotes.Domain.Contract.Authorization;
using RemoteNotes.Domain.Contract.Navigation;
using RemoteNotes.Domain.Core.Attributes;
using RemoteNotes.Domain.Core.Constants;
using RemoteNotes.Domain.Exceptions.Authorization;
using RemoteNotes.Domain.Extensions;
using RemoteNotes.Domain.Services.ViewModel;
using RemoteNotes.Service.Client.Contract.Authorization;
using RemoteNotes.UI.ViewModel.Tool;

namespace RemoteNotes.UI.ViewModel.Authorization
{
    [ViewModelRegistration(NavigationTag = PageTags.SignUp)]
    public class SignUpViewModel : BaseNavigationViewModel
    {
        private readonly IAuthorizationHolder _authorizationHolder;
        private readonly IAuthorizationService _authorizationService;
        
        public SignUpViewModel(
            INavigationService navigationService,
            IUserDialogs userDialogs,
            IAuthorizationHolder authorizationHolder,
            IAuthorizationService authorizationService)
            : base(navigationService, userDialogs)
        {
            _authorizationHolder = authorizationHolder;
            _authorizationService = authorizationService;
            
            Title = "Sign up";
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
        
        private string _confirmPassword;
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => SetProperty(ref _confirmPassword, value);
        }
        
        public ICommand SignUpCommand => new AsyncCommand(() => CommandExecutionHolderMethodAsync(OnSignUpCommand));
        
        public ICommand ToLoginCommand => new AsyncCommand(() => CommandExecutionHolderMethodAsync(OnToLoginCommand));

        private Task OnToLoginCommand()
        {
            return NavigationService.NavigateWithReplaceAsync(PageTags.Login, CancellationToken.None);
        }

        private async Task OnSignUpCommand()
        {
            var login = Login;
            var password = Password;
            var confirmPassword = ConfirmPassword;

            try
            {
                await SignUpAsync(login, password, confirmPassword);
            }
            catch (AuthorizationException authenticationException)
            {
                ShowToast("Sign up error. Check, if you have user account");
            }
            catch (InvalidDataException invalidDataException)
            {
                ShowToast("Input data not matched the rules");
            }
            catch (ArgumentNullException argumentNullException)
            {
                ShowToast("Credentials cant be empty");
            }
        }

        private async Task SignUpAsync(string login, string password, string confirmPassword)
        {
            if (!password.Equals(confirmPassword))
            {
                ShowToast("Passwords should be the same");
                return;
            }
            
            await _authorizationService.SignUpAsync(login, password, CancellationToken.None);

            if (_authorizationHolder.GetLastSession().IsValid())
                await NavigationService.NavigateWithReplaceAsync(PageTags.Dashboard, CancellationToken.None);
            else
                await ShowAlertAsync("Cant login with given credentials");
        }
    }
}