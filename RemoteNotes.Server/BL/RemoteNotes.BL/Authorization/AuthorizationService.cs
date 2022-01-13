using System;
using System.IO;
using System.Threading.Tasks;
using RemoteNotes.BL.Contract.Authorization;
using RemoteNotes.BL.Contract.Password;
using RemoteNotes.BL.Contract.User;
using RemoteNotes.BL.Contract.UserToken;
using RemoteNotes.Domain.Models;
using RemoteNotes.Rules.Contract;

namespace RemoteNotes.BL.Authorization
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IUserService _userService;
        private readonly IUserTokenService _userTokenService;
        private readonly IPasswordCryptor _passwordCryptor;
        private readonly IAuthorizationDataValidator _dataValidator;

        public AuthorizationService(
            IUserService userService,
            IUserTokenService userTokenService,
            IPasswordCryptor passwordCryptor,
            IAuthorizationDataValidator dataValidator)
        {
            _userService = userService;
            _userTokenService = userTokenService;
            _passwordCryptor = passwordCryptor;
            _dataValidator = dataValidator;
        }
        
        public async Task<TokenModel> SignUpAsync(string username, string password)
        {
            ValidateUserData(username, password);
            var userModel = await _userService.CreateUserAsync(username, _passwordCryptor.ToPassword(password));
            return _userTokenService.CreateToken(userModel);
        }
        
        public async Task<TokenModel> SignInAsync(string username, string password)
        {
            var user = await _userService.GetUserAsync(username);
            ValidateUser(user, password);
            var userModel = await _userService.GetUserProfileAsync(username);
            return _userTokenService.CreateToken(userModel);
        }

        public Task<TokenModel> RefreshTokenAsync(string refreshToken)
        {
            return Task.FromResult(_userTokenService.RefreshToken(refreshToken));
        }

        private void ValidateUser(Domain.Entity.User userData, string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password));
            
            if (_passwordCryptor.IsPasswordsEquals(userData.EncryptedPassword, password))
                throw new InvalidDataException("Invalid password");
        }

        private void ValidateUserData(string username, string password)
        {
            _dataValidator.ValidateLogin(username);
            _dataValidator.ValidatePassword(password);
        }
    }
}