using System.Threading;
using System.Threading.Tasks;

namespace RemoteNotes.Service.Client.Contract.Authorization
{
    public interface IAuthorizationService
    {
        bool IsAuthorized { get; }
        
        Task SignInAsync(string userName, string password, CancellationToken token);
        
        Task SignUpAsync(string userName, string password, CancellationToken token);

        Task UpdateSessionAsync(string sessionToken, CancellationToken token);
    }
}