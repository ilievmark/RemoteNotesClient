using System;
using System.Threading.Tasks;

namespace RemoteNotes.DAL.Contract
{
    public interface ICRUDRepository<T>
    {
        Task<T> GetByIdAsync(Guid id);
    }
}