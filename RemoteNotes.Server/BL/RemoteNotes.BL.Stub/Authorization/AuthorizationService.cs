using System;
using System.Threading.Tasks;
using RemoteNotes.BL.Contract.Authorization;
using RemoteNotes.BL.Security.UserToken;
using RemoteNotes.Domain.Models;
using RemoteNotes.Rules.Contract;

namespace RemoteNotes.BL.Stub.Authorization
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IAuthorizationDataValidator _dataValidator;
        private readonly IUserTokenService _userTokenService;

        public AuthorizationService(
            IAuthorizationDataValidator dataValidator,
            IUserTokenService userTokenService)
        {
            _dataValidator = dataValidator;
            _userTokenService = userTokenService;
        }

        public Task<TokenModel> SignUpAsync(string username, string password)
        {
            ValidateUserData(username, password);
            var userModel = new UserModel {UserName = username};
            return Task.FromResult(
                _userTokenService.CreateToken(userModel));
        }

        public Task<TokenModel> SignInAsync(string username, string password)
        {
            ValidateUser(username, password);
            var userModel = new UserModel {UserName = username};
            return Task.FromResult(
                _userTokenService.CreateToken(userModel));
        }

        public Task<TokenModel> RefreshTokenAsync(string refreshToken)
        {
            return Task.FromResult(_userTokenService.RefreshToken(refreshToken));
        }
        
        private void ValidateUser(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException(nameof(userName));
            
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password));
        }

        private void ValidateUserData(string username, string password)
        {
            _dataValidator.ValidateLogin(username);
            _dataValidator.ValidatePassword(password);
        }
    }
}