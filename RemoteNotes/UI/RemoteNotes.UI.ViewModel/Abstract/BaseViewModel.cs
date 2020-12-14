using System.Threading.Tasks;

namespace RemoteNotes.UI.ViewModel.Abstract 
{
    public abstract class BaseViewModel : BindableBase, IInitialize
    {
        public virtual Task InitializeAsync() => Task.CompletedTask;
    }
}