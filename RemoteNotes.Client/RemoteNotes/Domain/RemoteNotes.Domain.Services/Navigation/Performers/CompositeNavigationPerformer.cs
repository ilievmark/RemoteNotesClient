using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RemoteNotes.Domain.Core.Enums;
using RemoteNotes.Domain.Contract.Navigation;
using RemoteNotes.Domain.Core.Navigation;
using Xamarin.Forms;
using System;

namespace RemoteNotes.Domain.Services.Navigation.Performers
{
    public class CompositeNavigationPerformer : ICompositeNavigationPerformer
    {
        private readonly IReadOnlyDictionary<ENavigationDirrection, INavigationPerformer> _navigationPerformers;

        public CompositeNavigationPerformer(
            IEnumerable<INavigationPerformer> navigationPerformers)
        {
            _navigationPerformers = navigationPerformers.ToDictionary(p => p.Dirrection);
        }

        public Task PerformNavigationAsync(ENavigationDirrection dirrection, Page page, NavigationData data, CancellationToken token)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(NavigationData));

            if (!_navigationPerformers.ContainsKey(dirrection))
                throw new NotSupportedException(dirrection.ToString());

            if (page == null)
                return Task.CompletedTask;

            return _navigationPerformers[dirrection].PerformNavigationAsync(page, data, token);
        }
    }
}
