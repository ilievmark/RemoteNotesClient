using RemoteNotes.Domain.Response;

namespace RemoteNotes.Service.Client.Contract.Authorization
{
    public interface IAuthorizationUpdater
    {
        void SetData(AuthorizationResponse data);
    }
}