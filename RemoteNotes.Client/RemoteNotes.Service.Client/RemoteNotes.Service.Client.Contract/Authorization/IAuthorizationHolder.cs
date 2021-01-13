using RemoteNotes.Domain.Response;

namespace RemoteNotes.Service.Client.Contract.Authorization
{
    public interface IAuthorizationHolder
    {
        bool IsAuthorized { get; }

        AuthorizationResponse GetData();
    }
}