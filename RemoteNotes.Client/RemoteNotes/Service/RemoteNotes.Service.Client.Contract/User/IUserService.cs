using System.Threading.Tasks;
using RemoteNotes.Domain.Models;

namespace RemoteNotes.Service.Client.Contract.User
{
    public interface IUserService
    {
        Task<UserModel> GetUserDataAsync();

        Task<UserModel> UpdateUserDataAsync(UserModel user);
    }
}