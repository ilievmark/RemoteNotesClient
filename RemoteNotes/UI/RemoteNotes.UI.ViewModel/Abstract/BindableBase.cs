using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RemoteNotes.UI.ViewModel.Abstract 
{
    public abstract class BindableBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            OnPropertyChanged(propertyName);
        }

        protected void SetProperty<TProperty>(ref TProperty propertyField, TProperty newValue, [CallerMemberName] string propertyName = null)
        {
            propertyField = newValue;
            RaisePropertyChanged(propertyName);
        }
    }
}