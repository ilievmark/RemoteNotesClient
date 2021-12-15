using RemoteNotes.Domain.Models;

namespace RemoteNotes.Domain.Contract.Authorization
{
    public interface IAuthorizationUpdater
    {
        void SaveSession(TokenModel session);
    }
}