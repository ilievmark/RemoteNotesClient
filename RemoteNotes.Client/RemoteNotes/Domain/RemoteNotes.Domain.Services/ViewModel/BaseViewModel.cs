using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Acr.UserDialogs;
using RemoteNotes.Domain.Core.Exceptions;
using RemoteNotes.Domain.Exceptions.Authorization;

namespace RemoteNotes.Domain.Services.ViewModel
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        protected readonly IUserDialogs UserDialogs;

        public BaseViewModel(IUserDialogs userDialogs)
        {
            UserDialogs = userDialogs;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void SetProperty<T>(T value, ref T field, [CallerMemberName] string propertyName = null)
        {
            field = value;
            SendPropertyChangedEvent(propertyName);
        }

        protected void SendPropertyChangedEvent(string propertyName)
        {
            var args = new PropertyChangedEventArgs(propertyName);
            PropertyChanged?.Invoke(this, args);
            OnPropertyChanged(args);
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
        {
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
                UserDialogs.Alert("Your session is expired, pls relogin to continue");
            }
            catch (NoInternetException iex)
            {
                UserDialogs.Alert("No internet connection, pls check connection and repeat you request");
            }
            catch (Exception ex)
            {
                Debugger.Break();
                throw;
            }

            afterMainAction?.Invoke();
        }
    }
}
