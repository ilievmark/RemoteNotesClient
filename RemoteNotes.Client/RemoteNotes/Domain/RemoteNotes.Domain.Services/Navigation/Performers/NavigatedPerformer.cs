using System.Threading;
using System.Threading.Tasks;
using RemoteNotes.Domain.Contract.Navigation;
using RemoteNotes.Domain.Contract.ViewModel;
using RemoteNotes.Domain.Core.Enums;
using RemoteNotes.Domain.Core.Navigation;
using Xamarin.Forms;

namespace RemoteNotes.Domain.Services.Navigation.Performers
{
    public class NavigatedPerformer : INavigationPerformer
    {
        public ENavigationDirrection Dirrection => ENavigationDirrection.Navigated;

        public Task PerformNavigationAsync(Page page, NavigationData data, CancellationToken token)
        {
            if (page.BindingContext is INavigated navigationViewModel)
                return navigationViewModel.OnNavigatedAsync(data, token);
            return Task.CompletedTask;
        }
    }
}
