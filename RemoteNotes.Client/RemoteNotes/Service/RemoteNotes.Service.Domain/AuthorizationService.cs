using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using RemoteNotes.Domain.Extensions;
using RemoteNotes.Domain.Requests;
using RemoteNotes.Domain.Response;
using RemoteNotes.Service.Client.Contract.Authorization;

namespace RemoteNotes.Service.Domain
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly HttpClient _httpClient;
        private readonly IAuthorizationHolder _authorizationHolder;
        private readonly IAuthorizationUpdater _authorizationUpdater;

        public AuthorizationService(
            HttpClient httpClient,
            IAuthorizationHolder authorizationHolder,
            IAuthorizationUpdater authorizationUpdater)
        {
            _httpClient = httpClient;
            _authorizationHolder = authorizationHolder;
            _authorizationUpdater = authorizationUpdater;
        }

        public bool IsAuthorized => _authorizationHolder.IsAuthorized;

        public async Task<AuthorizationResponse> LoginAsync(LoginRequest authModel)
        {
            if (authModel == null)
                throw new ArgumentNullException(nameof(authModel));

            var result = await _httpClient.PostAsync(Constants.BaseUrl + "/login", authModel, CancellationToken.None);
            var authorizationResult = await result.ReadAsJsonAsync<AuthorizationResponse>();
            _authorizationUpdater.SetData(authorizationResult);
            return authorizationResult;
        }

        public Task LogoutAsync()
        {
            _authorizationUpdater.SetData(null);
            return Task.CompletedTask;
        }
    }
}
