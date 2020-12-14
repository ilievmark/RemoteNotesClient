using System.Threading.Tasks;
using RemoteNotes.UI.ViewModel.Abstract;
using RemoteNotes.UI.ViewModel.Service;
using Xamarin.Forms;

namespace RemoteNotes.UI.Shell.Navigation
{
    public class NavigationController : INavigationController
    {
        private readonly NavigationProvider _navigationProvider;
        
        private INavigation Navigation => _navigationProvider.GetNavigation();

        public NavigationController(NavigationProvider navigationProvider)
        {
            _navigationProvider = navigationProvider;
        }

        public async Task NavigateToAsync(string pageKey)
        {
            var page = App.Resolve<ContentPage>(pageKey);
            if (page.BindingContext is IInitialize pageViewModel)
                await pageViewModel.InitializeAsync();
            await Navigation.PushAsync(page);
        }

        public Task NavigateBackAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}