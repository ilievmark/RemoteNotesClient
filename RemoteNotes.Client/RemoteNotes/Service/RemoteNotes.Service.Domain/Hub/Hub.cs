using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using RemoteNotes.Domain.Exceptions;
using RemoteNotes.Service.Client.Contract.Hub;

namespace RemoteNotes.Service.Domain.Hub
{
    public class Hub : IHubConnection, IHubMessager, IHubObservable
    {
        private readonly IHubAuthorizationProvider _authorizationProvider;
        private readonly string _hubUrl;
        
        private HubConnection _hubConnection;

        public event Action ConnectionStatusChanged = delegate { };
        
        public bool IsConnected => _hubConnection?.State == HubConnectionState.Connected;

        public Hub(
            IHubAuthorizationProvider authorizationProvider,
            string hubUrl)
        {
            _authorizationProvider = authorizationProvider;
            _hubUrl = hubUrl;
        }
        
        public async Task ConnectAsync()
        {
            await DisconnectAsync();
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(_hubUrl, options => options.AccessTokenProvider = _authorizationProvider.GetTokenAsync)
                .Build();
            _hubConnection.Closed += OnClosedAsync;
            _hubConnection.Reconnecting += OnReconectingAsync;
            _hubConnection.Reconnected += OnReconectedAsync;
            await _hubConnection.StartAsync();
        }

        public async Task DisconnectAsync()
        {
            if (_hubConnection != null)
            {
                _hubConnection.Closed -= OnClosedAsync;
                _hubConnection.Reconnecting -= OnReconectingAsync;
                _hubConnection.Reconnected -= OnReconectedAsync;
                await _hubConnection.StopAsync();
                await _hubConnection.DisposeAsync();
                _hubConnection = null;
            }
        }

        public Task<TResult> SendMessageAsync<TParam, TResult>(string messageTag, TParam param)
        {
            if (!IsConnected)
                throw new HubNotConnectedException(_hubUrl);
            return _hubConnection.InvokeCoreAsync<TResult>(messageTag, new object[] { param });
        }

        public void Subscribe(IHubObserver observer)
        {
            _hubConnection.On<string>(observer.MessageTag, observer.HandleMessageAsync);
        }

        public void Unsubscribe(IHubObserver observer)
        {
            _hubConnection.Remove(observer.MessageTag);
        }

        private Task OnClosedAsync(Exception e)
        {
            Debugger.Break();
            ConnectionStatusChanged();
            return Task.CompletedTask;
        }

        private Task OnReconectingAsync(Exception e)
        {
            Debugger.Break();
            ConnectionStatusChanged();
            return Task.CompletedTask;
        }

        private Task OnReconectedAsync(string e)
        {
            Debugger.Break();
            ConnectionStatusChanged();
            return Task.CompletedTask;
        }
    }
}