using System.Threading.Tasks;
using RemoteNotes.DAL.Models;

namespace RemoteNotes.DAL.Contract
{
    public interface IUserRepository : ICRUDRepository<UserRead>
    {
        Task<UserRead> GetUserAsync(string username);
    }
}