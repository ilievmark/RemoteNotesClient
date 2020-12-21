using RemoteNotes.UI.ViewModel.Abstract;
using Xamarin.Forms;

namespace RemoteNotes.UI.Control
{
    public class BasePage<TViewModel> : ContentPage
        where TViewModel : BaseViewModel
    {
        public BasePage(TViewModel viewModel)
        {
            BindingContext = viewModel;
        }
    }
}