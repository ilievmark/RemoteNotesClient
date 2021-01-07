using System.Threading.Tasks;
using RemoteNotes.Service.Client.Contract.Model;

namespace RemoteNotes.Service.Client.Contract.Hub
{
    public interface IAuthorizationHub
    {
        Task<AuthResult> LoginAsync(AuthModel authModel);
    }
}