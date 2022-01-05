using System.Collections.Generic;
using System.Threading.Tasks;

namespace RemoteNotes.BL.Contract.Note
{
    public interface INoteService
    {
        Task<List<Domain.Entity.Note>> GetNotesAsync(int userId);
    }
}