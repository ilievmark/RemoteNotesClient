using System.Net.Http;
using System.Threading.Tasks;
using RemoteNotes.API.Contract;
using RemoteNotes.Domain;
using RemoteNotes.Domain.Contract.Authorization;
using RemoteNotes.Domain.Core.Constants;
using RemoteNotes.Domain.Endpoints;
using RemoteNotes.Domain.Extensions;
using RemoteNotes.Domain.Models;

namespace RemoteNotes.API
{
    public class UserApi : IUserApi
    {
        private readonly IAuthorizationHolder _authorizationHolder;
        private readonly HttpClient _httpClient;

        public UserApi(
            IAuthorizationHolder authorizationHolder,
            HttpClient httpClient)
        {
            _authorizationHolder = authorizationHolder;
            _httpClient = httpClient;
        }

        public Task<ApiResponse<UserModel>> GetUserDataAsync()
            => _httpClient
                .CreateRequest()
                .ByResource(ProjectConstants.BaseUrl, $"/{UserApis.GetUser}")
                .WithMethod(HttpMethod.Get)
                .WithAuthorization(AuthTypes.Bearer, _authorizationHolder.GetLastSession().Token)
                .MakeRequestAsync()
                .ReadAsJsonAsync<ApiResponse<UserModel>>();

        public Task<ApiResponse<UserModel>> UpdateUserDataAsync(UserModel user)
            => _httpClient
                .CreateRequest()
                .ByResource(ProjectConstants.BaseUrl, $"/{UserApis.Update}")
                .WithMethod(HttpMethod.Post)
                .WithAuthorization(AuthTypes.Bearer, _authorizationHolder.GetLastSession().Token)
                .MakeRequestAsync()
                .ReadAsJsonAsync<ApiResponse<UserModel>>();
    }
}
