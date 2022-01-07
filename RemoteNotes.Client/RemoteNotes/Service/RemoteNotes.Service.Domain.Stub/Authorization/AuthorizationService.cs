using System;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using RemoteNotes.API.Contract;
using RemoteNotes.Domain.Contract.Authorization;
using RemoteNotes.Domain.Exceptions.Authorization;
using RemoteNotes.Domain.Extensions;
using RemoteNotes.Domain.Models;
using RemoteNotes.Domain.Requests;
using RemoteNotes.Rules.Contract;
using RemoteNotes.Service.Client.Contract.Authorization;

namespace RemoteNotes.Service.Domain.Stub.Authorization
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IAuthorizationHolder _authorizationHolder;
        private readonly IAuthorizationUpdater _authorizationUpdater;
        private readonly IAuthorizationDataValidator _authorizationDataValidator;

        private SignInRequest _acceptableRequest;
        
        public bool IsAuthorized => _authorizationHolder.GetLastSession().IsValid();

        public AuthorizationService(
            IAuthorizationHolder authorizationHolder,
            IAuthorizationUpdater authorizationUpdater,
            IAuthorizationDataValidator authorizationDataValidator)
        {
            _authorizationHolder = authorizationHolder;
            _authorizationUpdater = authorizationUpdater;
            _authorizationDataValidator = authorizationDataValidator;
            
            _acceptableRequest = new SignInRequest
            {
                Username = "JonieJi29",
                Password = "12345t67890"
            };
        }

        public async Task SignInAsync(string userName, string password, CancellationToken token)
        {
            await Task.Delay(2000);
            
            if (_acceptableRequest.Username != userName)
                throw new AuthenticationException();
            
            if (_acceptableRequest.Password != password)
                throw new AuthenticationException();
            
            var tokenModel = new TokenModel
            {
                Token = "sdfsdfsdfs",
                ExpireAt = DateTimeOffset.Now.AddMinutes(30),
                CanBeUpdatedTill = DateTimeOffset.Now.AddDays(3)
            };
            
            _authorizationUpdater.SaveSession(tokenModel);
        }

        public async Task SignUpAsync(string userName, string password, CancellationToken token)
        {
            await Task.Delay(2000);
            
            _authorizationDataValidator.ValidateLogin(userName);
            _authorizationDataValidator.ValidatePassword(password);
            
            if (_acceptableRequest.Username == userName)
                throw new AuthorizationException();

            var tokenModel = new TokenModel
            {
                Token = "sdfsdfsdfs",
                ExpireAt = DateTimeOffset.Now.AddMinutes(30),
                CanBeUpdatedTill = DateTimeOffset.Now.AddDays(3)
            };
            
            _authorizationUpdater.SaveSession(tokenModel);
        }

        public async Task UpdateSessionAsync(string sessionToken, CancellationToken token)
        {
            await Task.Delay(2000);
            
            if (!_authorizationHolder.GetLastSession().CanUpdate())
                throw new AuthorizationException();

            var tokenModel = new TokenModel
            {
                Token = "sdfsdfsdfs",
                ExpireAt = DateTimeOffset.Now.AddMinutes(30),
                CanBeUpdatedTill = DateTimeOffset.Now.AddDays(3)
            };
            
            _authorizationUpdater.SaveSession(tokenModel);
        }
    }
}