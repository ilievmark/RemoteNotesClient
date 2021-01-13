using System;
using Newtonsoft.Json;
using RemoteNotes.Domain.Response;
using RemoteNotes.Service.Client.Contract.Authorization;
using Xamarin.Essentials;

namespace RemoteNotes.UI.Shell.Service
{
    public class AuthorizationHolder : IAuthorizationHolder, IAuthorizationUpdater
    {
        public bool IsAuthorized => GetData()?.TokenModel?.ExpireAt > DateTime.UtcNow;

        public AuthorizationResponse GetData()
            => JsonConvert.DeserializeObject<AuthorizationResponse>(Preferences.Get(nameof(AuthorizationResponse), string.Empty));

        public void SetData(AuthorizationResponse data)
            => Preferences.Set(nameof(AuthorizationResponse), JsonConvert.SerializeObject(data));
    }
}