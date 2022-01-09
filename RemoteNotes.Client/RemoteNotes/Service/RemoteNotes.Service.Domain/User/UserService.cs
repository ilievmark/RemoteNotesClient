using System.Threading.Tasks;
using RemoteNotes.API.Contract;
using RemoteNotes.Domain.Exceptions;
using RemoteNotes.Domain.Extensions;
using RemoteNotes.Domain.Models;
using RemoteNotes.Service.Client.Contract.User;

namespace RemoteNotes.Service.Domain.User
{
    public class UserService : IUserService
    {
        private readonly IUserApi _userApi;

        public UserService(IUserApi userApi)
        {
            _userApi = userApi;
        }
        
        public async Task<UserModel> GetUserDataAsync()
        {
            var response = await _userApi.GetUserDataAsync();
            
            if (response.Status.IsFailure())
                throw new ServerUnhandledException(response.Message);

            return response.Result;
        }

        public async Task<UserModel> UpdateUserDataAsync(UserModel user)
        {
            var response = await _userApi.UpdateUserDataAsync(user);
            
            if (response.Status.IsFailure())
                throw new ServerUnhandledException(response.Message);

            return response.Result;
        }
    }
}