using System.Collections.Generic;
using System.Security.Claims;
using RemoteNotes.Domain.Entity;
using RemoteNotes.Domain.Security;

namespace RemoteNotes.BL.Security.UserToken
{
    public interface IUserTokenService
    {
        TokenModel CreateToken(User user);

        string UserId(IEnumerable<Claim> claims);

        TokenModel RefreshToken(string token);
    }
}