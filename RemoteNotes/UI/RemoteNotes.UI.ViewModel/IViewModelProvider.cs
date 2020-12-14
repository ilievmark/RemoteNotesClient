namespace RemoteNotes.UI.ViewModel
{
    public interface IViewModelProvider
    {
        TViewModel GetViewModel<TViewModel>() where TViewModel : BaseViewModel;
    }
}