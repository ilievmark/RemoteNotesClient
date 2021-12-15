using System.Threading.Tasks;
using RemoteNotes.Domain.Requests;
using RemoteNotes.Domain.Response;

namespace RemoteNotes.Service.Client.Contract.Authorization
{
    public interface IAuthorizationService
    {
        bool IsAuthorized { get; }
        
        Task<AuthorizationResponse> LoginAsync(LoginRequest authModel);

        Task LogoutAsync();
    }
}