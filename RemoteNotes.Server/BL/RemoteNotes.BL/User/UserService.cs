using System.Threading.Tasks;
using RemoteNotes.BL.Contract.User;
using RemoteNotes.DAL.Contract;
using RemoteNotes.Domain.Models;

namespace RemoteNotes.BL.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserModel> CreateUserAsync(string userName, string password)
        {
            var user = new Domain.Entity.User
            {
                Username = userName,
                EncryptedPassword = password
            };
            await _userRepository.AddAsync(user);
            return ToUserModel(user);
        }

        public async Task<UserModel> GetUserProfileAsync(string username)
        {
            var user = await GetUserAsync(username);
            return ToUserModel(user);
        }

        public Task<Domain.Entity.User> GetUserAsync(string username)
        {
            return _userRepository.GetUserAsync(username);
        }

        public async Task<UserModel> UpdateProfileAsync(UserModel userModel)
        {
            var user = await GetUserAsync(userModel.UserName);
            UpdateUserFromProfile(user, userModel);
            _userRepository.Update(user, true);
            return userModel;
        }

        private void UpdateUserFromProfile(Domain.Entity.User userEntity, UserModel userProfile)
        {
            
        }

        private UserModel ToUserModel(Domain.Entity.User user)
        {
            return new UserModel();
        }
    }
}
