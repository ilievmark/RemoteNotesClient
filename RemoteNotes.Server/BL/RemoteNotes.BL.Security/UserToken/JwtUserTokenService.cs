using System.Collections.Generic;
using System.Linq;
using RemoteNotes.BL.Security.Token;
using RemoteNotes.DAL.Models;

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

        public string UserId(string token)
            => GetFormToken(UserIdKey, token);

        public string UserName(string token)
            => GetFormToken(UserNameKey, token);

        public bool IsTokenValid(string token)
            => _tokenService.IsTokenValid(token);

        public TokenModel RefreshToken(string token, IDictionary<string, string> claimsLookUp)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region -- Private methods --

        private IDictionary<string, string> UserClaims(UserRead user)
            => new Dictionary<string, string>
            {
                { UserIdKey, user.Id.ToString() },
                { UserNameKey, user.Username }
            };

        private string GetFormToken(string key, string token)
        {
            var claims = _tokenService.GetClaims(token);

            return claims.First(x => x.Type == key).Value;
        }

        #endregion
    }
}