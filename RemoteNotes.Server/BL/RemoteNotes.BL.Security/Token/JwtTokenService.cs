using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using RemoteNotes.Domain.Security;

namespace RemoteNotes.BL.Security.Token
{
    public class JwtTokenService : ITokenService
    {
        private const string BEARER_PREFIX = "Bearer ";

        #region -- ITokenService implementation --

        public TokenModel CreateToken(IDictionary<string, string> claimsLookUp)
        {
            var now = DateTime.UtcNow;
            var expireTime = now.Add(TimeSpan.FromMinutes(AuthOptions.TokenLifetimeMinutes));
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.Issuer,
                audience: AuthOptions.Audience,
                notBefore: now,
                claims: claimsLookUp.Any() ? claimsLookUp.Select(x => new Claim(x.Key, x.Value)) : null,
                expires: expireTime,
                signingCredentials: new SigningCredentials(AuthOptions.SymmetricSecurityKey, SecurityAlgorithms.HmacSha256));

            return new TokenModel
            {
                Token = new JwtSecurityTokenHandler().WriteToken(jwt),
                ExpireAt = expireTime
            };
        }

        public TokenModel RefreshToken(string token, IDictionary<string, string> claimsLookUp)
        {
            var handler = new JwtSecurityTokenHandler();
            bool isValid;
            try
            {
                handler.ValidateToken(RemovePrefix(token), new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = true,

                    ValidIssuer = AuthOptions.Issuer,
                    ValidAudience = AuthOptions.Audience,
                    IssuerSigningKey = AuthOptions.SymmetricSecurityKey

                }, out var securityKey);

                isValid = (securityKey != null);
            }
            catch
            {
                isValid = false;
            }

            if (!isValid)
                throw new Exception("Invalid token");

            return CreateToken(claimsLookUp);
        }

        public IEnumerable<Claim> GetClaims(string token)
        {
            if (!IsTokenValid(token))
                throw new Exception("Token is invalid");

            var handler = new JwtSecurityTokenHandler();

            if (!(handler.ReadToken(RemovePrefix(token)) is JwtSecurityToken jwtSecurityToken))
                return Enumerable.Empty<Claim>();

            return jwtSecurityToken.Claims;
        }

        public bool IsTokenValid(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            bool isValid;
            try
            {
                handler.ValidateToken(RemovePrefix(token), new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = true,

                    ValidIssuer = AuthOptions.Issuer,
                    ValidAudience = AuthOptions.Audience,
                    IssuerSigningKey = AuthOptions.SymmetricSecurityKey

                }, out var securityKey);

                isValid = (securityKey != null);
            }
            catch
            {
                isValid = false;
            }

            return isValid;
        }

        public bool IsTokenExpired(string token, DateTime now)
        {
            if (!IsTokenValid(token))
                throw new Exception("Token is invalid");

            var handler = new JwtSecurityTokenHandler();
            if (!(handler.ReadToken(RemovePrefix(token)) is JwtSecurityToken jwtSecurityToken))
                throw new Exception("Token is invalid");

            return !(jwtSecurityToken.ValidFrom <= now && now <= jwtSecurityToken.ValidTo);
        }

        #endregion
        #region -- Private methods --

        private static string RemovePrefix(string token)
            => token.Replace(BEARER_PREFIX, string.Empty);

        #endregion
    }
}
