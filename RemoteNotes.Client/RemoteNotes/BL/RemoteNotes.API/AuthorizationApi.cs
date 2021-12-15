using System.Net.Http;
using System.Threading.Tasks;
using RemoteNotes.API.Contract;
using RemoteNotes.Domain;
using RemoteNotes.Domain.Contract.Authorization;
using RemoteNotes.Domain.Core.Constants;
using RemoteNotes.Domain.Extensions;
using RemoteNotes.Domain.Requests;
using RemoteNotes.Domain.Response;

namespace RemoteNotes.API
{
    public class AuthorizationApi : IAuthorizationApi
    {
        private readonly IAuthorizationHolder _authorizationHolder;
        private readonly HttpClient _httpClient;

        public AuthorizationApi(
            IAuthorizationHolder authorizationHolder,
            HttpClient httpClient)
        {
            _authorizationHolder = authorizationHolder;
            _httpClient = httpClient;
        }
        
        public Task<ApiResponse<AuthorizationResponse>> SignInAsync(SignInRequest request)
            => _httpClient
                .CreateRequest()
                .ByResource(ProjectConstants.BaseUrl, "/signIn")
                .WithMethod(HttpMethod.Post)
                .WithContent(request)
                .MakeRequestAsync()
                .ReadAsJsonAsync<ApiResponse<AuthorizationResponse>>();

        public Task<ApiResponse<AuthorizationResponse>> SignUpAsync(SignUpRequest request)
            => _httpClient
                .CreateRequest()
                .ByResource(ProjectConstants.BaseUrl, "/signUp")
                .WithMethod(HttpMethod.Post)
                .WithContent(request)
                .MakeRequestAsync()
                .ReadAsJsonAsync<ApiResponse<AuthorizationResponse>>();

        public Task<ApiResponse<AuthorizationResponse>> UpdateAuthorizationAsync()
            => _httpClient
                .CreateRequest()
                .ByResource(ProjectConstants.BaseUrl, "/update")
                .WithMethod(HttpMethod.Post)
                .WithAuthorization(AuthTypes.Bearer, _authorizationHolder.GetLastSession().Token)
                .MakeRequestAsync()
                .ReadAsJsonAsync<ApiResponse<AuthorizationResponse>>();
    }
}