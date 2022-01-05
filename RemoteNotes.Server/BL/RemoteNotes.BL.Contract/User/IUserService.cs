using System.Threading.Tasks;
using RemoteNotes.Domain.Models;

namespace RemoteNotes.BL.Contract.User
{
    public interface IUserService
    {
        Task<UserModel> CreateUserAsync(string userName, string password);

        Task<UserModel> GetUserProfileAsync(string username);
        
        Task<Domain.Entity.User> GetUserAsync(string username);
        
        Task<UserModel> UpdateProfileAsync(UserModel user);
    }
}
