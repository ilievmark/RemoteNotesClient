using System;
using System.Threading.Tasks;

namespace RemoteNotes.UI.ViewModel.Abstract 
{
    public abstract class BaseViewModel : BindableBase
    {
        protected Action CreateCommandHandler(Action handler) => () => handler();
        protected Func<Task> CreateAsyncCommandHandler(Func<Task> handler) => () => handler();
    }
}