using System;
using System.Security.Authentication;
using System.Threading.Tasks;
using RemoteNotes.API.Contract;
using RemoteNotes.Domain;
using RemoteNotes.Domain.Exceptions.Authorization;
using RemoteNotes.Domain.Models;
using RemoteNotes.Domain.Requests;
using RemoteNotes.Domain.Response;

namespace RemoteNotes.API.Stub
{
    public class AuthorizationApi : IAuthorizationApi
    {
        private SignInRequest _acceptableRequest;
        
        public AuthorizationApi()
        {
            _acceptableRequest = new SignInRequest
            {
                Username = "JonieJi29",
                Password = "12345t67890"
            };
        }
        
        public async Task<ApiResponse<AuthorizationResponse>> SignInAsync(SignInRequest request)
        {
            await Task.Delay(2000);
            
            if (_acceptableRequest.Username != request.Username)
                throw new AuthenticationException();
            
            if (_acceptableRequest.Password != request.Password)
                throw new AuthenticationException();
            
            var response = new ApiResponse<AuthorizationResponse>();
            var token = new TokenModel
            {
                Token = "sdfsdfsdfs",
                ExpireAt = DateTimeOffset.Now.AddMinutes(30),
                CanBeUpdatedTill = DateTimeOffset.Now.AddDays(3)
            };
            response.SetSuccess(new AuthorizationResponse {TokenModel = token});
            return response;
        }

        public async Task<ApiResponse<AuthorizationResponse>> SignUpAsync(SignUpRequest request)
        {
            await Task.Delay(2000);
            
            if (_acceptableRequest.Username == request.Username)
                throw new AuthorizationException();

            var response = new ApiResponse<AuthorizationResponse>();
            var token = new TokenModel
            {
                Token = "sdfsdfsdfs",
                ExpireAt = DateTimeOffset.Now.AddMinutes(30),
                CanBeUpdatedTill = DateTimeOffset.Now.AddDays(3)
            };
            response.SetSuccess(new AuthorizationResponse {TokenModel = token});
            return response;
        }

        public async Task<ApiResponse<AuthorizationResponse>> UpdateAuthorizationAsync()
        {
            await Task.Delay(2000);
            
            var response = new ApiResponse<AuthorizationResponse>();
            var token = new TokenModel
            {
                Token = "sdfsdfsdfs",
                ExpireAt = DateTimeOffset.Now.AddMinutes(30),
                CanBeUpdatedTill = DateTimeOffset.Now.AddDays(3)
            };
            response.SetSuccess(new AuthorizationResponse {TokenModel = token});
            return response;
        }
    }
}