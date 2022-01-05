using System.Collections.Generic;
using System.Security.Claims;
using RemoteNotes.Domain.Models;

namespace RemoteNotes.BL.Security.UserToken
{
    public interface IUserTokenService
    {
        TokenModel CreateToken(UserModel user);

        int UserId(IEnumerable<Claim> claims);
        
        string UserName(string token);

        TokenModel RefreshToken(string token);
    }
}