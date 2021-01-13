using System.Threading.Tasks;
using RemoteNotes.Domain.Entity;

namespace RemoteNotes.DAL.Contract
{
    public interface IUserRepository : ICRUDRepository<User>
    {
        Task<User> GetUserAsync(string username);
    }
}