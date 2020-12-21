using System.Threading.Tasks;

namespace RemoteNotes.UI.ViewModel.Service
{
    public interface INavigationController
    {
        Task NavigateToAsync(string pageKey);

        Task NavigateToRootWith(string pageKey);
    }
}