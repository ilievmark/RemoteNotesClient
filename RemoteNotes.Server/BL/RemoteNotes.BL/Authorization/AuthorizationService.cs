using System;
using System.IO;
using System.Threading.Tasks;
using RemoteNotes.BL.Security.Password;
using RemoteNotes.BL.Security.UserToken;
using RemoteNotes.DAL.Contract;
using RemoteNotes.DAL.Models;
using RemoteNotes.Domain.Security;

namespace RemoteNotes.BL.Authorization
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IUserTokenService _userTokenService;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordCryptor _passwordCryptor;

        public AuthorizationService(
            IUserTokenService userTokenService,
            IUserRepository userRepository,
            IPasswordCryptor passwordCryptor)
        {
            _userTokenService = userTokenService;
            _userRepository = userRepository;
            _passwordCryptor = passwordCryptor;
        }
        
        public async Task<TokenModel> LogInAsync(string username, string password)
        {
            var user = await _userRepository.GetUserAsync(username);
            ValidateUser(user, password);
            return _userTokenService.CreateToken(user);
        }

        private void ValidateUser(UserRead userData, string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password));
            
            if (_passwordCryptor.IsPasswordsEquals(userData.EncryptedPassword, password))
                throw new InvalidDataException("Invalid password");
        }
    }
}