using Newtonsoft.Json;
using RemoteNotes.Service.Client.Contract;
using RemoteNotes.Service.Client.Contract.Model;
using Xamarin.Essentials;

namespace RemoteNotes.UI.Shell.Service
{
    public class AuthorizationHolder : IAuthorizationHolder, IAuthorizationUpdater
    {
        public bool IsAuthorized => GetData()?.Success ?? false;

        public AuthResult GetData()
            => JsonConvert.DeserializeObject<AuthResult>(Preferences.Get(nameof(AuthResult), string.Empty));

        public void SetData(AuthResult data)
            => Preferences.Set(nameof(AuthResult), JsonConvert.SerializeObject(data));
    }
}