using System.Collections.Generic;

namespace RemoteNotes.Domain.Core.Navigation
{
    public class NavigationData
    {
        public IDictionary<string, object> Parameters { get; }

        public NavigationData(IDictionary<string, object> parameters)
        {
            Parameters = parameters;
        }
    }
}
