using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RemoteNotes.Domain.Contract.Navigation
{
    public interface INavigationService
    {
        Task NavigateToRootAsync(CancellationToken token, params KeyValuePair<string, object>[] parameters);

        Task NavigateNextAsync(string tag, CancellationToken token, params KeyValuePair<string, object>[] parameters);

        Task NavigateWithReplaceAsync(string tag, CancellationToken token, params KeyValuePair<string, object>[] parameters);

        Task NavigateBackAsync(CancellationToken token, params KeyValuePair<string, object>[] parameters);
    }
}
