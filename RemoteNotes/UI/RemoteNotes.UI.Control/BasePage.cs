using RemoteNotes.UI.ViewModel.Abstract;
using RemoteNotes.UI.ViewModel.Service;
using Xamarin.Forms;

namespace RemoteNotes.UI.Control
{
    public class BasePage<TViewModel> : ContentPage
        where TViewModel : BaseViewModel
    {
        public BasePage(IViewModelProvider vmProvider)
        {
            BindingContext = vmProvider.GetViewModel<TViewModel>();
        }
    }
}