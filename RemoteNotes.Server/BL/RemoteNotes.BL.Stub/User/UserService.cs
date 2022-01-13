using System;
using System.Threading.Tasks;
using RemoteNotes.BL.Contract.User;
using RemoteNotes.Domain.Models;

namespace RemoteNotes.BL.Stub.User
{
    public class UserService : IUserService
    {
        public event Action<int, UserModel> UserDataChanged = delegate { };
        
        public async Task<UserModel> CreateUserAsync(string userName, string password)
        {
            await Task.Delay(2000);
            return new UserModel {UserName = userName};
        }

        public async Task<UserModel> GetUserProfileAsync(string username)
        {
            await Task.Delay(2000);
            return new UserModel {UserName = username};
        }

        public async Task<Domain.Entity.User> GetUserAsync(string username)
        {
            await Task.Delay(2000);
            return new Domain.Entity.User {UserName = username};
        }

        public async Task<UserModel> UpdateProfileAsync(UserModel user)
        {
            await Task.Delay(2000);
            UserDataChanged(user.Id, user);
            return user;
        }
    }
}