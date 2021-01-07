using System.Threading.Tasks;

namespace RemoteNotes.Service.Client.Contract.Hub
{
    public interface IHubConnection
    {
        bool IsConnected { get; }
        
        Task ConnectAsync();

        Task DisconnectAsync();
    }
}