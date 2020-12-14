using RemoteNotes.UI.ViewModel;

namespace RemoteNotes.UI.Shell.Navigation
{
    public class ViewModelProvider : IViewModelProvider
    {
        public TViewModel GetViewModel<TViewModel>()
            where TViewModel : BaseViewModel
                => App.Resolve<TViewModel>();
    }
}