using System.Threading.Tasks;

namespace RemoteNotes.Service.Client.Contract.Hub
{
    public interface IHubObserver
    {
        string HubName { get; }
        
        string MessageTag { get; }

        Task HandleMessageAsync(string json);
    }
}