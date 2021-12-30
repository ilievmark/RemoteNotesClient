using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RemoteNotes.Service.Client.Contract.Hub;

namespace RemoteNotes.Service.Domain.Hub
{
    public class HubController : IHubController
    {
        private readonly IHubConnection _hubConnection;
        private readonly IHubObservable _hubObservable;
        private readonly IHubReconnector _hubReconnector;
        private readonly IList<IHubObserver> _hubObservers;

        public HubController(
            IHubConnection hubConnection,
            IHubObservable hubObservable,
            IHubReconnector hubReconnector,
            IList<IHubObserver> hubObservers,
            string hubName)
        {
            _hubConnection = hubConnection;
            _hubObservable = hubObservable;
            _hubReconnector = hubReconnector;
            _hubObservers = hubObservers.Where(o => o.HubName == hubName).ToList();
        }
        
        public Task StartAsync()
        {
            if (_hubConnection.IsConnected)
                return Task.CompletedTask;
            
            foreach (var observer in _hubObservers)
                _hubObservable.Subscribe(observer);
            
            _hubReconnector.StartObserveForReconnection(_hubConnection);
            
            return _hubConnection.ConnectAsync();
        }

        public Task StopAsync()
        {
            if (!_hubConnection.IsConnected)
                return Task.CompletedTask;
            
            foreach (var observer in _hubObservers)
                _hubObservable.Unsubscribe(observer);

            _hubReconnector.StopObserveForReconnection(_hubConnection);
            
            return _hubConnection.DisconnectAsync();
        }
    }
}