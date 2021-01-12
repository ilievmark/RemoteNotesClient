using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using RemoteNotes.BL.Security.Token;
using RemoteNotes.DAL.Models;
using RemoteNotes.Domain.Security;

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

        public TokenModel CreateToken(UserRead user)
            => _tokenService.CreateToken(UserClaims(user));

        public string UserId(IEnumerable<Claim> claims)
            => claims.First(x => x.Type == UserIdKey).Value;

        public TokenModel RefreshToken(string token)
            => _tokenService.RefreshToken(token);

        #endregion

        #region -- Private methods --

        private IDictionary<string, string> UserClaims(UserRead user)
            => new Dictionary<string, string>
            {
                { UserIdKey, user.Id.ToString() },
                { UserNameKey, user.Username }
            };

        #endregion
    }
}