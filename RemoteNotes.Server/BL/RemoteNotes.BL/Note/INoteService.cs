using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RemoteNotes.BL.Note
{
    public interface INoteService
    {
        Task<IEnumerable<DAL.Models.Note>> GetNotesAsync(Guid userId);
    }
}