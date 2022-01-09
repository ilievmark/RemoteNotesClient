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
            _user = new UserModel
            {
                Id = 0,
                PhotoUrl = "https://i1.mosconsv.ru/287/400/800/90/kotek_iosif.jpg",
                Name = "Jhon",
                Surname = "Doe",
                Email = "jhon.doe.2925@gmail.com",
                UserName = "JonieJi29"
            };
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
