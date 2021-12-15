using Acr.UserDialogs;
using RemoteNotes.Domain.Contract.Navigation;

namespace RemoteNotes.Domain.Services.ViewModel
{
    public abstract class BaseNavigationViewModel : BaseViewModel
    {
        protected INavigationService NavigationService { get; }

        public BaseNavigationViewModel(
            INavigationService navigationService,
            IUserDialogs userDialogs) : base(userDialogs)
        {
            NavigationService = navigationService;
        }
    }
}
