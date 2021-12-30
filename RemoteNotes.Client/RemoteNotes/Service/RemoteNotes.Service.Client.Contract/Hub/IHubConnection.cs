using System;
using System.Threading.Tasks;

namespace RemoteNotes.Service.Client.Contract.Hub
{
    public interface IHubConnection
    {
        event Action ConnectionStatusChanged;
        
        bool IsConnected { get; }
        
        Task ConnectAsync();

        Task DisconnectAsync();
    }
}