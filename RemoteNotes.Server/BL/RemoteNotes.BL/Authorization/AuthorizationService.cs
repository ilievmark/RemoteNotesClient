using System;
using System.IO;
using System.Threading.Tasks;
using RemoteNotes.BL.Security.Password;
using RemoteNotes.BL.Security.UserToken;
using RemoteNotes.DAL.Contract;
using RemoteNotes.Domain.Entity;
using RemoteNotes.Domain.Security;
using RemoteNotes.Rules.Contract;

namespace RemoteNotes.BL.Authorization
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IUserTokenService _userTokenService;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordCryptor _passwordCryptor;
        private readonly IAuthorizationDataValidator _dataValidator;

        public AuthorizationService(
            IUserTokenService userTokenService,
            IUserRepository userRepository,
            IPasswordCryptor passwordCryptor,
            IAuthorizationDataValidator dataValidator)
        {
            _userTokenService = userTokenService;
            _userRepository = userRepository;
            _passwordCryptor = passwordCryptor;
            _dataValidator = dataValidator;
        }
        
        public async Task<TokenModel> SignUpAsync(string username, string password)
        {
            ValidateUserData(username, password);
            var user = new User
            {
                Username = username,
                EncryptedPassword = _passwordCryptor.ToPassword(password)
            };
            await _userRepository.AddAsync(user);
            return _userTokenService.CreateToken(user);
        }
        
        public async Task<TokenModel> LogInAsync(string username, string password)
        {
            var user = await _userRepository.GetUserAsync(username);
            ValidateUser(user, password);
            return _userTokenService.CreateToken(user);
        }

        public Task<TokenModel> RefreshTokenAsync(string refreshToken)
            => Task.FromResult(_userTokenService.RefreshToken(refreshToken));

        private void ValidateUser(User userData, string password)
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