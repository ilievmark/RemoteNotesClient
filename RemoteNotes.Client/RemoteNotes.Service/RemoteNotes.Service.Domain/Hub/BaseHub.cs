using RemoteNotes.Service.Client.Contract.Hub;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;

namespace RemoteNotes.Service.Domain.Hub
{
    public abstract class BaseHub : IHubConnection
    {
        private bool _initialized;
        protected HubConnection _hubConnection;

        public bool IsConnected => _hubConnection?.State == HubConnectionState.Connected;

        protected abstract HubConnection BuildConnection();

        protected abstract void SubscribeEvents(HubConnection hubConnection);

        protected void Initialize()
        {
            if (_initialized)
            {
                _hubConnection?.StopAsync();
                _hubConnection?.DisposeAsync();
            }

            _hubConnection = BuildConnection();
            SubscribeEvents(_hubConnection);
            _initialized = true;
        }

        public Task ConnectAsync()
        {
            if (!_initialized)
                Initialize();
            return _hubConnection.StartAsync();
        }

        public Task DisconnectAsync()
        {
            if (!_initialized)
                Initialize();
            return _hubConnection.StopAsync();
        }
    }
}