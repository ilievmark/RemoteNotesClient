using System.Collections.Generic;
using System.Security.Claims;
using RemoteNotes.DAL.Models;
using RemoteNotes.Domain.Security;

namespace RemoteNotes.BL.Security.UserToken
{
    public interface IUserTokenService
    {
        TokenModel CreateToken(UserRead user);

        string UserId(IEnumerable<Claim> claims);

        string UserName(IEnumerable<Claim> claims);

        bool IsTokenValid(string token);

        TokenModel RefreshToken(string token, IDictionary<string, string> claimsLookUp);
    }
}