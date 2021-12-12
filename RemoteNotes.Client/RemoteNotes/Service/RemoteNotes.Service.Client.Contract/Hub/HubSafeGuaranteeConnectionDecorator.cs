using System;
using System.Diagnostics;
using System.Threading.Tasks;
using RemoteNotes.Service.Client.Contract.Hub;

namespace RemoteNotes.Service.Client.Contract.Hub
{
    public class HubSafeGuaranteeConnectionDecorator : IHubConnection
    {
        private readonly IHubConnection _decored;

        public HubSafeGuaranteeConnectionDecorator(IHubConnection decored)
        {
            _decored = decored;
        }

        public bool IsConnected => _decored.IsConnected;
        
        public async Task ConnectAsync()
        {
            while (!IsConnected)
            {
                await TryConnect();
            }
        }

        public async Task DisconnectAsync()
        {
            while (IsConnected)
            {
                await TryDisconnect();
            }
        }

        private async Task TryConnect()
        {
            try
            {
                await _decored.ConnectAsync();
            }
            catch (Exception e)
            {
                Debugger.Break();
                Debug.WriteLine("Some specific exception must be handled here instead of Exception overall");
            }
        }

        private async Task TryDisconnect()
        {
            try
            {
                await _decored.DisconnectAsync();
            }
            catch (Exception e)
            {
                Debugger.Break();
                Debug.WriteLine("Some specific exception must be handled here instead of Exception overall");
            }
        }
    }
}