using System;
using System.Collections.Generic;
using System.Security.Claims;
using RemoteNotes.Domain.Security;

namespace RemoteNotes.BL.Security.Token
{
    public interface ITokenService
    {
        TokenModel CreateToken(IDictionary<string, string> claimsLookUp);

        IEnumerable<Claim> GetClaims(string token);

        bool IsTokenValid(string token);

        TokenModel RefreshToken(string token);

        bool IsTokenExpired(string token, DateTime byDate);
    }
}
