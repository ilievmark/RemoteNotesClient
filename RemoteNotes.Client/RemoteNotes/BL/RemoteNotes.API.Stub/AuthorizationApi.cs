using System;
using System.Threading.Tasks;
using RemoteNotes.API.Contract;
using RemoteNotes.Domain;
using RemoteNotes.Domain.Models;
using RemoteNotes.Domain.Requests;
using RemoteNotes.Domain.Response;

namespace RemoteNotes.API.Stub
{
    public class AuthorizationApi : IAuthorizationApi
    {
        public async Task<ApiResponse<AuthorizationResponse>> SignInAsync(SignInRequest request)
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

        public async Task<ApiResponse<AuthorizationResponse>> SignUpAsync(SignUpRequest request)
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