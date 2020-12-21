using RemoteNotes.Service.Client.Contract.Model;

namespace RemoteNotes.Service.Client.Contract
{
    public interface IAuthorizationHolder
    {
        bool IsAuthorized { get; }

        AuthResult GetData();
    }
}