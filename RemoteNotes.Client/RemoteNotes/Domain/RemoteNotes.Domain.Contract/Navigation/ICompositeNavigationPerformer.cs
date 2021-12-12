using System.Threading;
using System.Threading.Tasks;
using RemoteNotes.Domain.Core.Enums;
using RemoteNotes.Domain.Core.Navigation;
using Xamarin.Forms;

namespace RemoteNotes.Domain.Contract.Navigation
{
    public interface ICompositeNavigationPerformer
    {
        Task PerformNavigationAsync(ENavigationDirrection dirrection, Page page, NavigationData data, CancellationToken token);
    }
}
