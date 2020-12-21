using System;
using System.Threading.Tasks;

namespace RemoteNotes.UI.ViewModel.Abstract 
{
    public abstract class BaseViewModel : BindableBase
    {
        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
        
        protected Action CreateCommandHandler(Action handler) => () => handler();
        
        protected Func<Task> CreateAsyncCommandHandler(Func<Task> handler) => () => handler();
    }
}