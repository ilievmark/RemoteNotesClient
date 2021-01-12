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

        TokenModel RefreshToken(string token);
    }
}