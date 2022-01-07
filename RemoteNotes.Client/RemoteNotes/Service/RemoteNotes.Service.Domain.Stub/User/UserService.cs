using System.Threading.Tasks;
using RemoteNotes.Domain.Models;
using RemoteNotes.Service.Client.Contract.User;

namespace RemoteNotes.Service.Domain.Stub.User
{
    public class UserService : IUserService
    {
        private readonly UserHub _userHub;
        
        private UserModel _user;
        
        public UserService(IUserHub userHub)
        {
            _userHub = (UserHub)userHub;
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
        
        public async Task<UserModel> GetUserDataAsync()
        {
            await Task.Delay(2000);
            return _user;
        }

        public async Task<UserModel> UpdateUserDataAsync(UserModel user)
        {
            await Task.Delay(2000);
            _user.Email = user.Email;
            _user.Name = user.Name;
            _user.Surname = user.Surname;
            _user.UserName = user.UserName;
            _user.PhotoUrl = user.PhotoUrl;
            _userHub.FireAction(_user);
            return _user;
        }
    }
}