using System.Threading.Tasks;
using RemoteNotes.Service.Client.Contract.Hub;
using RemoteNotes.Service.Client.Contract.Model;

namespace RemoteNotes.Service.Domain.Hub
{
    public class AuthorizationHub : BaseHub, IHubConnection, IAuthorizationHub
    {
        public bool IsConnected { get; }
        
        public Task ConnectAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task DisconnectAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<AuthResult> LoginAsync(AuthModel authModel)
        {
            throw new System.NotImplementedException();
        }
    }
}