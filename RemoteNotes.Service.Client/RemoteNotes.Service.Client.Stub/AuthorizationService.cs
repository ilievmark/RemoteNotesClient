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
        private bool _authorizedState;

        public AuthorizationService(
            IHubConnection authHubConnection,
            IAuthorizationHub authorizationHub)
        {
            _authHubConnection = authHubConnection;
            _authorizationHub = authorizationHub;
        }
        
        public Task<AuthResult> LoginAsync(AuthModel authModel)
        {
            if (_authorizedState)
                throw new InvalidOperationException("Already authorized");
                
            if (authModel == null)
                throw new ArgumentNullException(nameof(authModel));
            
            if (string.IsNullOrEmpty(authModel.Login))
                throw new InvalidDataException("Login is invalid");
            
            if (string.IsNullOrEmpty(authModel.Password))
                throw new InvalidDataException("Password is invalid");

            if (!_authHubConnection.IsConnected)
                _authHubConnection.ConnectAsync();

            _authorizedState = true;
            return _authorizationHub.LoginAsync(authModel);
        }

        public Task LogoutAsync()
        {
            if (!_authorizedState)
                throw new InvalidOperationException("Must be authorized first");

            _authorizedState = false;
            return Task.CompletedTask;
        }
    }
}