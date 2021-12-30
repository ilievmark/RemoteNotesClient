using System.Threading.Tasks;

namespace RemoteNotes.Service.Client.Contract.Hub
{
    public interface IHubController
    {
        Task StartAsync();

        Task StopAsync();
    }
}