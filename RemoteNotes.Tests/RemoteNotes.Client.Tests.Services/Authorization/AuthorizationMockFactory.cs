using RemoteNotes.API.Stub;
using RemoteNotes.Rules;
using RemoteNotes.Service.Domain.Authorization;
using RemoteNotes.Service.Domain.Stub.Authorization;
using AuthorizationService = RemoteNotes.Service.Domain.Authorization.AuthorizationService;

namespace RemoteNotes.Client.Tests.Services.Authorization
{
    public static class AuthorizationMockFactory
    {
        public static AuthorizationService GetAuthService(AuthorizationHolder holder)
        {
            return new AuthorizationService(
                new AuthorizationApi(),
                holder,
                holder,
                new AuthorizationDataValidator());
        }

        public static AuthorizationHolder GetAuthHolder()
        {
            return new AuthorizationHolder();
        }
    }
}