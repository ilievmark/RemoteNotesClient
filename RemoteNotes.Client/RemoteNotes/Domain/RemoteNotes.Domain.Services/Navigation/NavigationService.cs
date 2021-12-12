using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RemoteNotes.Domain.Contract.Navigation;
using RemoteNotes.Domain.Core.Enums;
using RemoteNotes.Domain.Core.Extensions;
using RemoteNotes.Domain.Core.Navigation;
using RemoteNotes.Domain.Services.RootCasts;
using Xamarin.Forms;

namespace RemoteNotes.Domain.Services.Navigation
{
    public class NavigationService : INavigationService
    {
        private readonly NavigationProvider _navigationProvider;
        private readonly ICompositeNavigationPerformer _navigationPerformer;
        private readonly IPageBuilder _pageBuilder;
        private readonly IViewModelBuilder _viewModelBuilder;

        private INavigation Navigation => _navigationProvider.GetNavigation();

        public NavigationService(
            NavigationProvider navigationProvider,
            ICompositeNavigationPerformer navigationPerformer,
            IViewModelBuilder viewModelBuilder,
            IPageBuilder pageBuilder)
        {
            _navigationProvider = navigationProvider;
            _navigationPerformer = navigationPerformer;
            _pageBuilder = pageBuilder;
            _viewModelBuilder = viewModelBuilder;
        }

        public async Task NavigateToRootAsync(CancellationToken token, params KeyValuePair<string, object>[] parameters)
        {
            var data = GetNavigationData(parameters);

            for (int i = Navigation.NavigationStack.Count - 1; i > 0; i--)
            {
                var page = Navigation.CurrentPage();

                await _navigationPerformer.PerformNavigationAsync(ENavigationDirrection.NavigatingFrom, page, data, token);
                await Navigation.PopAsync();
                await _navigationPerformer.PerformNavigationAsync(ENavigationDirrection.NavigatedBack, page, data, token);
            }

            var currentPage = Navigation.CurrentPage();
            await _navigationPerformer.PerformNavigationAsync(ENavigationDirrection.NavigatedBack, currentPage, data, token);
        }

        public async Task NavigateBackAsync(CancellationToken token, params KeyValuePair<string, object>[] parameters)
        {
            var data = GetNavigationData(parameters);
            var previousPage = Navigation.PreviousPage();
            var currentPage = Navigation.CurrentPage();

            await _navigationPerformer.PerformNavigationAsync(ENavigationDirrection.NavigatingFrom, currentPage, data, token);
            token.ThrowIfCancellationRequested();
            await _navigationPerformer.PerformNavigationAsync(ENavigationDirrection.NavigatingBack, previousPage, data, token);
            token.ThrowIfCancellationRequested();

            await Navigation.PopAsync();
            token.ThrowIfCancellationRequested();

            await _navigationPerformer.PerformNavigationAsync(ENavigationDirrection.NavigatedFrom, currentPage, data, token);
            token.ThrowIfCancellationRequested();
            await _navigationPerformer.PerformNavigationAsync(ENavigationDirrection.NavigatedBack, previousPage, data, token);
        }

        public async Task NavigateNextAsync(string tag, CancellationToken token, params KeyValuePair<string, object>[] parameters)
        {
            var data = GetNavigationData(parameters);
            var currentPage = Navigation.CurrentPage();
            var nextPage = _pageBuilder.BuildPage(tag);
            var nextPageViewModel = _viewModelBuilder.BuildViewModel(tag);

            nextPage.BindingContext = nextPageViewModel;

            await _navigationPerformer.PerformNavigationAsync(ENavigationDirrection.NavigatingFrom, currentPage, data, token);
            token.ThrowIfCancellationRequested();
            await _navigationPerformer.PerformNavigationAsync(ENavigationDirrection.Navigating, nextPage, data, token);
            token.ThrowIfCancellationRequested();

            await Navigation.PushAsync(nextPage);
            token.ThrowIfCancellationRequested();

            await _navigationPerformer.PerformNavigationAsync(ENavigationDirrection.NavigatedFrom, currentPage, data, token);
            token.ThrowIfCancellationRequested();
            await _navigationPerformer.PerformNavigationAsync(ENavigationDirrection.Navigated, nextPage, data, token);
        }

        public async Task NavigateWithReplaceAsync(string tag, CancellationToken token, params KeyValuePair<string, object>[] parameters)
        {
            var data = GetNavigationData(parameters);
            var currentPage = Navigation.CurrentPage();
            var nextPage = _pageBuilder.BuildPage(tag);
            var nextPageViewModel = _viewModelBuilder.BuildViewModel(tag);

            nextPage.BindingContext = nextPageViewModel;

            await _navigationPerformer.PerformNavigationAsync(ENavigationDirrection.NavigatingFrom, currentPage, data, token);
            token.ThrowIfCancellationRequested();
            await _navigationPerformer.PerformNavigationAsync(ENavigationDirrection.Navigating, nextPage, data, token);
            token.ThrowIfCancellationRequested();

            Navigation.InsertPageBefore(nextPage, currentPage);
            await Navigation.PopAsync();

            token.ThrowIfCancellationRequested();

            await _navigationPerformer.PerformNavigationAsync(ENavigationDirrection.NavigatedFrom, currentPage, data, token);
            token.ThrowIfCancellationRequested();
            await _navigationPerformer.PerformNavigationAsync(ENavigationDirrection.Navigated, nextPage, data, token);
        }

        private NavigationData GetNavigationData(KeyValuePair<string, object>[] parameters)
        {
            parameters = parameters ?? new KeyValuePair<string, object>[0];
            return new NavigationData(new Dictionary<string, object>(parameters));
        }
    }
}
