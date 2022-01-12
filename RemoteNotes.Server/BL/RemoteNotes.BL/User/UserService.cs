using System;
using System.Threading.Tasks;
using RemoteNotes.BL.Contract.User;
using RemoteNotes.DAL.Contract;
using RemoteNotes.Domain.Models;

namespace RemoteNotes.BL.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public event Action<int, UserModel> UserDataChanged = delegate {};

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserModel> CreateUserAsync(string userName, string password)
        {
            var user = new Domain.Entity.User
            {
                UserName = userName,
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
            UserDataChanged(user.Id, userModel);
            return userModel;
        }

        private void UpdateUserFromProfile(Domain.Entity.User userEntity, UserModel userProfile)
        {
            userEntity.Email = userProfile.Email;
            userEntity.Name = userProfile.Name;
            userEntity.Surname = userProfile.Surname;
            userEntity.PhotoUrl = userProfile.PhotoUrl;
        }

        private UserModel ToUserModel(Domain.Entity.User user)
        {
            return new UserModel
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                PhotoUrl = user.PhotoUrl,
                UserName = user.UserName
            };
        }
    }
}
