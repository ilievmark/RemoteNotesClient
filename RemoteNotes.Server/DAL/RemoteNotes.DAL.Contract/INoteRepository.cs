using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RemoteNotes.DAL.Models;

namespace RemoteNotes.DAL.Contract
{
    public interface INoteRepository : ICRUDRepository<NoteRead>
    {
        Task<IEnumerable<NoteRead>> GetByUserIdAsync(Guid id);
    }
}