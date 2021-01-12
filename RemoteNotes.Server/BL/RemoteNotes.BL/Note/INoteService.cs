using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RemoteNotes.BL.Note
{
    public interface INoteService
    {
        Task<List<Domain.Entity.Note>> GetNotesAsync(Guid userId);
    }
}