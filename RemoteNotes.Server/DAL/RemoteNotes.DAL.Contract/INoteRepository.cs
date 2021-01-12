using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RemoteNotes.Domain.Entity;

namespace RemoteNotes.DAL.Contract
{
    public interface INoteRepository : ICRUDRepository<Note>
    {
        Task<List<Note>> GetByUserIdAsync(Guid id);
    }
}