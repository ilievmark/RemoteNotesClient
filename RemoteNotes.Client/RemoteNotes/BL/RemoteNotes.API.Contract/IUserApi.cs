using System.Threading.Tasks;
using RemoteNotes.Domain;
using RemoteNotes.Domain.Models;

namespace RemoteNotes.API.Contract
{
    public interface IUserApi
    {
        Task<ApiResponse<UserModel>> GetUserDataAsync();

        Task<ApiResponse<UserModel>> UpdateUserDataAsync(UserModel user);
    }
}
