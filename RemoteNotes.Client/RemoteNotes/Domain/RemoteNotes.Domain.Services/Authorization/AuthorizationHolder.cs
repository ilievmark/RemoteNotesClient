using System.Threading.Tasks;
using RemoteNotes.Domain.Contract.Authorization;
using RemoteNotes.Domain.Contract.Storage;
using RemoteNotes.Domain.Core.Constants;
using RemoteNotes.Domain.Models;
using RemoteNotes.Service.Client.Contract.Hub;

namespace RemoteNotes.Domain.Services.Authorization
{
    public class AuthorizationHolder : IAuthorizationHolder, IAuthorizationUpdater, IHubAuthorizationProvider
    {
        private readonly IPreferencesStorage _storage;

        public AuthorizationHolder(
            IPreferencesStorage storage)
        {
            _storage = storage;
        }

        public TokenModel GetLastSession()
            => _storage.Available(StorageKeys.TokenModel) ?
                _storage.Get<TokenModel>(StorageKeys.TokenModel) :
                null;

        public void SaveSession(TokenModel session)
            => _storage.Put(StorageKeys.TokenModel, session);

        public Task<string> GetTokenAsync()
            => Task.FromResult(GetLastSession()?.Token);
    }
}
