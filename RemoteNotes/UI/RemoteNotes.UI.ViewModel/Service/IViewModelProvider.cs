using RemoteNotes.UI.ViewModel.Abstract;

namespace RemoteNotes.UI.ViewModel.Service
{
    public interface IViewModelProvider
    {
        TViewModel GetViewModel<TViewModel>() where TViewModel : BaseViewModel;
    }
}