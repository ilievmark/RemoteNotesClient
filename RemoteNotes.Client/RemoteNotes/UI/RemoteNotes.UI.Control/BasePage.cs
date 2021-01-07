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

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            if (BindingContext is IAppearable casted)
                casted.Appeared();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            
            if (BindingContext is IDisappearable casted)
                casted.Disappeared();
        }
    }
}