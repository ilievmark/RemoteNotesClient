using System.Threading;
using System.Threading.Tasks;
using RemoteNotes.Domain.Security;

namespace RemoteNotes.Service.Client.Contract.Authorization
{
    public interface IAuthorizationService
    {
        bool IsAuthorized { get; }
        
        Task<TokenModel> SignInAsync(string userName, string password, CancellationToken token);

        Task<TokenModel> UpdateSessionAsync(string sessionToken, CancellationToken token);

        Task LogoutAsync();
    }
}