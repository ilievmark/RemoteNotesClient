using RemoteNotes.Domain.Security;

namespace RemoteNotes.Domain.Contract.Authorization
{
    public interface IAuthorizationHolder
    {
        TokenModel GetLastSession();

        void SaveSession(TokenModel session);
    }
}
