using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Acr.UserDialogs;
using RemoteNotes.Domain.Contract.Navigation;
using RemoteNotes.Domain.Core.Extensions;
using RemoteNotes.Domain.Extensions;
using RemoteNotes.Domain.Services.Navigation.Holders;
using RemoteNotes.Domain.Services.RootCasts;
using RemoteNotes.Service.Client.Contract.Authorization;
using IAuthorizationHolder = RemoteNotes.Domain.Contract.Authorization.IAuthorizationHolder;

namespace RemoteNotes.Domain.Services.Navigation
{
    public class AuthorizedNavigationServiceDecorator : INavigationService
    {
        private readonly NavigationProvider _navigationProvider;
        private readonly PageNavigationTypeHolder _pageNavigationTypeHolder;
        private readonly ViewModelNavigationTypeHolder _viewModelNavigationTypeHolder;
        private readonly IUserDialogs _userDialogs;
        private readonly IAuthorizationService _authorizationService;
        private readonly INavigationService _navigationService;
        private readonly IAuthorizationHolder _authorizationHolder;

        public AuthorizedNavigationServiceDecorator(
            PageNavigationTypeHolder pageNavigationTypeHolder,
            ViewModelNavigationTypeHolder viewModelNavigationTypeHolder,
            NavigationProvider navigationProvider,
            IUserDialogs userDialogs,
            IAuthorizationService authorizationService,
            IAuthorizationHolder authorizationHolder,
            INavigationService decoreeService)
        {
            _userDialogs = userDialogs;
            _navigationProvider = navigationProvider;
            _authorizationService = authorizationService;
            _pageNavigationTypeHolder = pageNavigationTypeHolder;
            _viewModelNavigationTypeHolder = viewModelNavigationTypeHolder;
            _navigationService = decoreeService;
            _authorizationHolder = authorizationHolder;
        }

        public async Task NavigateBackAsync(CancellationToken token, params KeyValuePair<string, object>[] parameters)
        {
            await UpdateAuthorizationAsync(token);

            var currentPage = _navigationProvider.GetNavigation().CurrentPage();
            if (!IsAuthorized() && IsAuthorizedNavigation(currentPage.GetType(), out var alternativeTag))
            {
                await _navigationService.NavigateToRootAsync(token, parameters);
                await _navigationService.NavigateWithReplaceAsync(alternativeTag, token, parameters);
            }
            else
            {
                await _navigationService.NavigateBackAsync(token, parameters);
            }
        }

        public async Task NavigateNextAsync(string tag, CancellationToken token, params KeyValuePair<string, object>[] parameters)
        {
            await UpdateAuthorizationAsync(token);

            if (!IsAuthorized() && IsAuthorizedNavigation(tag, out var alternativeTag))
            {
                await _navigationService.NavigateToRootAsync(token, parameters);
                await _navigationService.NavigateWithReplaceAsync(alternativeTag, token, parameters);
            }
            else
            {
                await _navigationService.NavigateNextAsync(tag, token, parameters);
            }
        }

        public async Task NavigateWithReplaceAsync(string tag, CancellationToken token, params KeyValuePair<string, object>[] parameters)
        {
            await UpdateAuthorizationAsync(token);

            if (!IsAuthorized() && IsAuthorizedNavigation(tag, out var alternativeTag))
            {
                await _navigationService.NavigateToRootAsync(token, parameters);
                await _navigationService.NavigateWithReplaceAsync(alternativeTag, token, parameters);
            }
            else
            {
                await _navigationService.NavigateWithReplaceAsync(tag, token, parameters);
            }
        }

        public async Task NavigateToRootAsync(CancellationToken token, params KeyValuePair<string, object>[] parameters)
        {
            var firstPage = _navigationProvider.GetNavigation().FirstPage();

            await UpdateAuthorizationAsync(token);

            if (!IsAuthorized() && IsAuthorizedNavigation(firstPage.GetType(), out var alternativeTag))
            {
                await _navigationService.NavigateToRootAsync(token, parameters);
                await _navigationService.NavigateWithReplaceAsync(alternativeTag, token, parameters);
            }
            else
            {
                await _navigationService.NavigateToRootAsync(token, parameters);
            }
        }

        private async Task UpdateAuthorizationAsync(CancellationToken token)
        {
            using (_userDialogs.Loading())
            {
                if (!IsAuthorized() && CanUpdateAuthorization())
                    await _authorizationService.UpdateSessionAsync(null, token);
            }
        }

        private bool IsAuthorizedNavigation(string tag, out string alternativeTag)
        {
            var pageTypeRegistration = _pageNavigationTypeHolder.GetPageTypeRegistration(tag);
            if (pageTypeRegistration.IsAuthorized())
            {
                alternativeTag = pageTypeRegistration.GetAlternativeNavigationTag();
                return true;
            }

            var viewModelTypeRegistration = _viewModelNavigationTypeHolder.GetViewModelTypeRegistration(tag);
            if (viewModelTypeRegistration.IsAuthorized())
            {
                alternativeTag = viewModelTypeRegistration.GetAlternativeNavigationTag();
                return true;
            }

            alternativeTag = string.Empty;
            return false;

        }

        private bool IsAuthorizedNavigation(Type type, out string alternativeTag)
        {
            var pageTypeRegistration = _pageNavigationTypeHolder.GetPageTypeRegistration(type);
            if (pageTypeRegistration.IsAuthorized())
            {
                alternativeTag = pageTypeRegistration.GetAlternativeNavigationTag();
                return true;
            }

            var viewModelTypeRegistration = _viewModelNavigationTypeHolder.GetViewModelTypeRegistration(type);
            if (viewModelTypeRegistration.IsAuthorized())
            {
                alternativeTag = viewModelTypeRegistration.GetAlternativeNavigationTag();
                return true;
            }

            alternativeTag = string.Empty;
            return false;
        }

        private bool IsAuthorized()
            => _authorizationHolder.GetLastSession().IsValid();

        private bool CanUpdateAuthorization()
            => _authorizationHolder.GetLastSession().CanUpdate();
    }
}
