using RemoteNotes.Domain.Models;

namespace RemoteNotes.Domain.Response
{
    public class AuthorizationResponse
    {
        public TokenModel TokenModel { get; set; }

        public AuthorizationResponse(TokenModel tokenModel)
        {
            TokenModel = tokenModel;
        }
    }
}
