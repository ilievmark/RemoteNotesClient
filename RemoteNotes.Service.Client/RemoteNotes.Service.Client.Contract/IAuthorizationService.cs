using System.Threading.Tasks;
using RemoteNotes.Service.Client.Contract.Model;

namespace RemoteNotes.Service.Client.Contract
{
    public interface IAuthorizationService
    {
        Task<AuthResult> LoginAsync(AuthModel authModel);

        Task LogoutAsync();
    }
}