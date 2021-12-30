using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Acr.UserDialogs;
using RemoteNotes.Domain.Exceptions;
using RemoteNotes.Domain.Exceptions.Authorization;

namespace RemoteNotes.Domain.Services.ViewModel
{
    public abstract class BaseViewModel : BindableBase
    {
        protected readonly IUserDialogs UserDialogs;

        public BaseViewModel(IUserDialogs userDialogs)
        {
            UserDialogs = userDialogs;
        }
        
        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        protected virtual async Task CommandExecutionHolderMethodAsync(
            Func<Task> commandDelegate,
            Action beforeMainAction = null,
            Action afterMainAction = null)
        {
            beforeMainAction?.Invoke();

            try
            {
                await commandDelegate();
            }
            catch (AuthorizationException aex)
            {
                ShowToast("Your session is expired, pls relogin to continue");
            }
            catch (NoInternetException iex)
            {
                ShowToast("No internet connection, pls check connection and repeat you request");
            }
            catch (Exception ex)
            {
                Debugger.Break();
                throw;
            }

            afterMainAction?.Invoke();
        }
        
        protected Task ShowAlertAsync(string message)
            => UserDialogs.AlertAsync(message);

        protected void ShowToast(string message)
            => UserDialogs.Toast(new ToastConfig(message));
    }
}
