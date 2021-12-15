using RemoteNotes.Domain.Models;

namespace RemoteNotes.Domain.Contract.Authorization
{
    public interface IAuthorizationHolder
    {
        TokenModel GetLastSession();
    }
}
