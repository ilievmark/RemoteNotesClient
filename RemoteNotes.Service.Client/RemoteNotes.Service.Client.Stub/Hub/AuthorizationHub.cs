using System.Threading.Tasks;
using RemoteNotes.Service.Client.Contract.Hub;
using RemoteNotes.Service.Client.Contract.Model;

namespace RemoteNotes.Service.Client.Stub.Hub
{
    public class AuthorizationHub : IHubConnection, IAuthorizationHub
    {
        public bool IsConnected => true;
        
        public Task ConnectAsync() => Task.CompletedTask;

        public Task DisconnectAsync() => Task.CompletedTask;
        
        public Task<AuthResult> LoginAsync(AuthModel authModel)
        {
            if (authModel.Login != "Silvester_222")
                return Task.FromResult(new AuthResult(false));
            if (authModel.Password != "Password_222")
                return Task.FromResult(new AuthResult(false));
            return Task.FromResult(new AuthResult(true));
        }
    }
}