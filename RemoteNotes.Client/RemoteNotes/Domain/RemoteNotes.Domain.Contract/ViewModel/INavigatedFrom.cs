using System.Threading;
using System.Threading.Tasks;
using RemoteNotes.Domain.Core.Navigation;

namespace RemoteNotes.Domain.Contract.ViewModel
{
    public interface INavigatedFrom
    {
        Task OnNavigatedFromAsync(NavigationData navigationData, CancellationToken token);
    }
}
