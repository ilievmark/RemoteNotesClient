using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RemoteNotes.Domain.Services.ViewModel
{
    public abstract class BindableBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
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
    }
}