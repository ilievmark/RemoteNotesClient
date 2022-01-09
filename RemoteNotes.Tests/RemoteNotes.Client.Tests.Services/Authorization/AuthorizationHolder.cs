using RemoteNotes.Domain.Contract.Authorization;
using RemoteNotes.Domain.Models;

namespace RemoteNotes.Client.Tests.Services.Authorization
{
    public class AuthorizationHolder : IAuthorizationHolder, IAuthorizationUpdater
    {
        private TokenModel _token;
        
        public TokenModel GetLastSession()
        {
            return _token;
        }

        public void SaveSession(TokenModel session)
        {
            _token = session;
        }
    }
}