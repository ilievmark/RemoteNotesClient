using System;
using System.IO;
using System.Threading.Tasks;
using RemoteNotes.Service.Client.Contract;
using RemoteNotes.Service.Client.Contract.Hub;
using RemoteNotes.Service.Client.Contract.Model;

namespace RemoteNotes.Service.Client.Stub
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IHubConnection _authHubConnection;
        private readonly IAuthorizationHub _authorizationHub;
        private readonly IAuthorizationUpdater _authorizationUpdater;
        
        private bool _authorizedState;

        public AuthorizationService(
            IHubConnection authHubConnection,
            IAuthorizationHub authorizationHub,
            IAuthorizationUpdater authorizationUpdater)
        {
            _authHubConnection = authHubConnection;
            _authorizationHub = authorizationHub;
            _authorizationUpdater = authorizationUpdater;
        }

        public bool IsAuthorized { get; }

        public async Task<AuthResult> LoginAsync(AuthModel authModel)
        {
            if (_authorizedState)
                throw new InvalidOperationException("Already authorized");
                
            if (authModel == null)
                throw new ArgumentNullException(nameof(authModel));
            
            if (!_authHubConnection.IsConnected)
                _authHubConnection.ConnectAsync();

            var authResult = await _authorizationHub.LoginAsync(authModel);
            _authorizedState = authResult.Success;
            _authorizationUpdater.SetData(authResult);
            return authResult;
        }

        public Task LogoutAsync()
        {
            if (!_authorizedState)
                throw new InvalidOperationException("Must be authorized first");

            _authorizationUpdater.SetData(null);
            _authorizedState = false;
            return Task.CompletedTask;
        }
    }
}