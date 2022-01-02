using System.Threading;
using System.Threading.Tasks;
using RemoteNotes.API.Contract;
using RemoteNotes.Domain.Contract.Authorization;
using RemoteNotes.Domain.Exceptions.Authorization;
using RemoteNotes.Domain.Extensions;
using RemoteNotes.Domain.Requests;
using RemoteNotes.Rules.Contract;
using RemoteNotes.Service.Client.Contract.Authorization;

namespace RemoteNotes.Service.Domain.Stub.Authorization
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IAuthorizationApi _authorizationApi;
        private readonly IAuthorizationHolder _authorizationHolder;
        private readonly IAuthorizationUpdater _authorizationUpdater;
        private readonly IAuthorizationDataValidator _authorizationDataValidator;

        public bool IsAuthorized => _authorizationHolder.GetLastSession().IsValid();

        public AuthorizationService(
            IAuthorizationApi authorizationApi,
            IAuthorizationHolder authorizationHolder,
            IAuthorizationUpdater authorizationUpdater,
            IAuthorizationDataValidator authorizationDataValidator)
        {
            _authorizationApi = authorizationApi;
            _authorizationHolder = authorizationHolder;
            _authorizationUpdater = authorizationUpdater;
            _authorizationDataValidator = authorizationDataValidator;
        }

        public async Task SignInAsync(string userName, string password, CancellationToken token)
        {
            var request = new SignInRequest {Username = userName, Password = password};
            var response = await _authorizationApi.SignInAsync(request);
            
            _authorizationUpdater.SaveSession(response.Result.TokenModel);
        }

        public async Task SignUpAsync(string userName, string password, CancellationToken token)
        {
            _authorizationDataValidator.ValidateLogin(userName);
            _authorizationDataValidator.ValidatePassword(password);

            var request = new SignUpRequest {Username = userName, Password = password};
            var response = await _authorizationApi.SignUpAsync(request);
            
            _authorizationUpdater.SaveSession(response.Result.TokenModel);
        }

        public async Task UpdateSessionAsync(string sessionToken, CancellationToken token)
        {
            if (!_authorizationHolder.GetLastSession().CanUpdate())
                throw new AuthorizationException();

            var response = await _authorizationApi.UpdateAuthorizationAsync();
            
            _authorizationUpdater.SaveSession(response.Result.TokenModel);
        }
    }
}