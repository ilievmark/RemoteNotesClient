using RemoteNotes.Service.Client.Contract.Model;

namespace RemoteNotes.Service.Client.Contract
{
    public interface IAuthorizationUpdater
    {
        void SetData(AuthResult data);
    }
}