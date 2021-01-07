using System.Collections.Generic;
using RemoteNotes.BL.Security.Token;
using RemoteNotes.DAL.Models;

namespace RemoteNotes.BL.Security.UserToken
{
    public interface IUserTokenService
    {
        TokenModel CreateToken(UserRead user);

        string UserId(string token);

        string UserName(string token);

        bool IsTokenValid(string token);

        TokenModel RefreshToken(string token, IDictionary<string, string> claimsLookUp);
    }
}