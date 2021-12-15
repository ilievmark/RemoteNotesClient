using System;
using System.Threading.Tasks;
using RemoteNotes.Domain.Requests;
using RemoteNotes.Domain.Response;
using RemoteNotes.Domain.Security;
using RemoteNotes.Service.Client.Contract.Authorization;

namespace RemoteNotes.Service.Client.Stub
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IAuthorizationUpdater _authorizationUpdater;

        public AuthorizationService(
            IAuthorizationUpdater authorizationUpdater)
        {
            _authorizationUpdater = authorizationUpdater;
        }

        public bool IsAuthorized { get; private set; }

        public async Task<AuthorizationResponse> LoginAsync(SignInRequest signInRequest)
        {
            if (signInRequest == null)
                throw new ArgumentNullException(nameof(signInRequest));

            var authResult = new AuthorizationResponse(new TokenModel());
            await Task.Delay(1000);
            IsAuthorized = true;
            _authorizationUpdater.SetData(authResult);
            return authResult;
        }

        public Task LogoutAsync()
        {
            _authorizationUpdater.SetData(null);
            IsAuthorized = false;
            return Task.CompletedTask;
        }
    }
}