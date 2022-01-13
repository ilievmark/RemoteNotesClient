using System;
using System.Threading.Tasks;
using RemoteNotes.BL.Contract.Authorization;
using RemoteNotes.BL.Contract.UserToken;
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

        public async Task<TokenModel> SignUpAsync(string username, string password)
        {
            await Task.Delay(2000);
            ValidateUserData(username, password);
            var userModel = new UserModel {UserName = username};
            return _userTokenService.CreateToken(userModel);
        }

        public async Task<TokenModel> SignInAsync(string username, string password)
        {
            await Task.Delay(2000);
            ValidateUser(username, password);
            var userModel = new UserModel {UserName = username};
            return _userTokenService.CreateToken(userModel);
        }

        public async Task<TokenModel> RefreshTokenAsync(string refreshToken)
        {
            await Task.Delay(2000);
            return _userTokenService.RefreshToken(refreshToken);
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