using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using RemoteNotes.BL.Security.Token;
using RemoteNotes.Domain.Entity;
using RemoteNotes.Domain.Models;

namespace RemoteNotes.BL.Security.UserToken
{
    public class JwtUserTokenService : IUserTokenService
    {
        private const string UserIdKey = nameof(UserIdKey);
        private const string UserNameKey = nameof(UserNameKey);

        private readonly ITokenService _tokenService;

        #region -- ITokenService implementation --

        public JwtUserTokenService(
            ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public TokenModel CreateToken(UserModel user)
            => _tokenService.CreateToken(UserClaims(user));

        public int UserId(IEnumerable<Claim> claims)
            => Convert.ToInt32(claims.First(x => x.Type == UserIdKey).Value);

        public string UserName(string token)
            => _tokenService.GetClaims(token).First(x => x.Type == UserNameKey).Value;

        public TokenModel RefreshToken(string token)
            => _tokenService.RefreshToken(token);

        #endregion

        #region -- Private methods --

        private IDictionary<string, string> UserClaims(UserModel user)
            => new Dictionary<string, string>
            {
                { UserIdKey, user.Id.ToString() },
                { UserNameKey, user.UserName }
            };

        #endregion
    }
}