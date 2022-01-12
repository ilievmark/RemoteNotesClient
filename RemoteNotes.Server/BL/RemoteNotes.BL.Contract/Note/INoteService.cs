using System.Collections.Generic;
using System.Threading.Tasks;
using RemoteNotes.Domain.Models;

namespace RemoteNotes.BL.Contract.Note
{
    public interface INoteService
    {
        Task<List<NoteModel>> GetNotesAsync(int userId);
        
        Task SaveNoteAsync(int userId, NoteModel note);
        
        Task DeleteNoteAsync(NoteModel note);
        
        Task<NoteModel> UpdateNoteAsync(NoteModel note);
    }
}