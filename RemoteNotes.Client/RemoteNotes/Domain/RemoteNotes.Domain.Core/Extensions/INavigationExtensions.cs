using System.Linq;
using Xamarin.Forms;

namespace RemoteNotes.Domain.Core.Extensions
{
    public static class INavigationExtensions
    {
        public static Page FirstPage(this INavigation navigation)
            => navigation.NavigationStack.FirstOrDefault();

        public static Page CurrentPage(this INavigation navigation)
            => navigation.NavigationStack.LastOrDefault();

        public static Page PreviousPage(this INavigation navigation)
            => navigation.NavigationStack.Count > 2 ?
                navigation.NavigationStack[navigation.NavigationStack.Count - 2] :
                null;
    }
}
