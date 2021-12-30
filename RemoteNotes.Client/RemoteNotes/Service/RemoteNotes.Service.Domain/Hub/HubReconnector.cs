using System.Threading.Tasks;
using RemoteNotes.Service.Client.Contract.Hub;

namespace RemoteNotes.Service.Domain.Hub
{
    public class HubReconnector : IHubReconnector
    {
        private readonly HubConfiguration _hubConfiguration;

        private IHubConnection _hubConnection;

        public HubReconnector(HubConfiguration hubConfiguration)
        {
            _hubConfiguration = hubConfiguration;
        }
        
        public void StartObserveForReconnection(IHubConnection hubConnection)
        {
            _hubConnection = hubConnection;
            hubConnection.ConnectionStatusChanged += OnHubConnectionChanged;
        }

        public void StopObserveForReconnection(IHubConnection hubConnection)
        {
            _hubConnection = null;
            hubConnection.ConnectionStatusChanged -= OnHubConnectionChanged;
        }

        private async void OnHubConnectionChanged()
        {
            if (!_hubConnection.IsConnected)
            {
                await Task.Delay(_hubConfiguration.ReconnectInMilliseconds);
                await _hubConnection.ConnectAsync();
            }
        }
    }
}