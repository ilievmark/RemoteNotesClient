using System.Threading;
using System.Threading.Tasks;
using RemoteNotes.Domain.Contract.Navigation;
using RemoteNotes.Domain.Contract.ViewModel;
using RemoteNotes.Domain.Core.Enums;
using RemoteNotes.Domain.Core.Navigation;
using Xamarin.Forms;

namespace RemoteNotes.Domain.Services.Navigation.Performers
{
    public class NavigatedBackPerformer : INavigationPerformer
    {
        public ENavigationDirrection Dirrection => ENavigationDirrection.NavigatedBack;

        public Task PerformNavigationAsync(Page page, NavigationData data, CancellationToken token)
        {
            if (page.BindingContext is INavigatedBack navigationViewModel)
                return navigationViewModel.OnNavigatedBackAsync(data, token);
            return Task.CompletedTask;
        }
    }
}
