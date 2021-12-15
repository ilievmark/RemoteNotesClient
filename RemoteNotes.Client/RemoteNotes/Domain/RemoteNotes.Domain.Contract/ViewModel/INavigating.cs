using System.Threading;
using System.Threading.Tasks;
using RemoteNotes.Domain.Core.Navigation;

namespace RemoteNotes.Domain.Contract.ViewModel
{
    public interface INavigating
    {
        Task OnNavigatingAsync(NavigationData navigationData, CancellationToken token);
    }
}
