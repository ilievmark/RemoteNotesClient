using RemoteNotes.API.Stub;
using RemoteNotes.Service.Domain.User;

namespace RemoteNotes.Client.Tests.Services.User
{
    public static class UserServiceMockFactory
    {
        public static UserService GetUserService()
        {
            return new UserService(
                new UserApi());
        }
    }
}