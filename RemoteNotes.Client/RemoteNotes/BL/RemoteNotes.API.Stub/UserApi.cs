using System.Threading.Tasks;
using RemoteNotes.API.Contract;
using RemoteNotes.Domain;
using RemoteNotes.Domain.Models;

namespace RemoteNotes.API.Stub
{
    public class UserApi : IUserApi
    {
        private UserModel _user;

        public UserApi()
        {
            _user = new UserModel();
        }

        public async Task<ApiResponse<UserModel>> GetUserDataAsync()
        {
            await Task.Delay(2000);
            var response = new ApiResponse<UserModel>();
            response.SetSuccess(_user);
            return response;
        }

        public Task<ApiResponse<UserModel>> UpdateUserDataAsync(UserModel user)
        {
            _user = user;
            return GetUserDataAsync();
        }
    }
}
