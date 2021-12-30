using System.Threading.Tasks;

namespace RemoteNotes.Service.Client.Contract.Hub
{
    public interface IHubAuthorizationProvider
    {
        Task<string> GetTokenAsync();
    }
}