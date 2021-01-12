using System;
using System.Threading.Tasks;

namespace RemoteNotes.DAL.Contract
{
    public interface ICRUDRepository<T>
    {
        Task AddAsync(T element, bool commit = true);
        Task<T> GetByIdAsync(Guid id);
    }
}