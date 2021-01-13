using System.Linq;
using System.Threading.Tasks;
using RemoteNotes.UI.ViewModel.Abstract;
using RemoteNotes.UI.ViewModel.Service;
using Xamarin.Forms;

namespace RemoteNotes.UI.Shell.Navigation
{
    public class NavigationController : INavigationController
    {
        private readonly NavigationProvider _navigationProvider;
        private readonly PageLocator _pageLocator;

        private INavigation Navigation => _navigationProvider.GetNavigation();

        public NavigationController(
            NavigationProvider navigationProvider,
            PageLocator pageLocator)
        {
            _navigationProvider = navigationProvider;
            _pageLocator = pageLocator;
        }

        public async Task NavigateToAsync(string pageKey)
        {
            var page = await ResolvePageAsync(pageKey);
            await Navigation.PushAsync(page);
        }

        public async Task NavigateToRootWith(string pageKey)
        {
            var page = await ResolvePageAsync(pageKey);
            var firstPage = Navigation.NavigationStack.First();
            Navigation.InsertPageBefore(page, firstPage);
            await Navigation.PopToRootAsync();
        }

        #region helpers

        private async Task<Page> ResolvePageAsync(string pageKey)
        {
            var page = _pageLocator.ResolveNamedPage(pageKey);
            await InitializeViewModelAsync(page);
            return page;
        }
        
        private Task InitializeViewModelAsync(Page page)
        {
            if (page.BindingContext is IInitialize initable)
                return initable.InitializeAsync();
            return Task.CompletedTask;
        }
        
        #endregion
    }
}