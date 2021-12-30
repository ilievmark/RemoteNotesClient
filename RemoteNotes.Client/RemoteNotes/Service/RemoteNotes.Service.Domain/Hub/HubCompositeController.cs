using System.Linq;
using System.Threading.Tasks;
using RemoteNotes.Service.Client.Contract.Hub;

namespace RemoteNotes.Service.Domain.Hub
{
    public class HubCompositeController : IHubController
    {
        private readonly IHubController[] _hubControllers;

        public HubCompositeController(
            params IHubController[] hubControllers)
        {
            _hubControllers = hubControllers;
        }
        
        public Task StartAsync()
        {
            return Task.WhenAll(
                _hubControllers.Select(h => h.StartAsync())
                );
        }

        public Task StopAsync()
        {
            return Task.WhenAll(
                _hubControllers.Select(h => h.StopAsync())
            );
        }
    }
}