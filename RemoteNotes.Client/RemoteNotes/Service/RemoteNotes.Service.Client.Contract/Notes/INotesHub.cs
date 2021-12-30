using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RemoteNotes.Domain.Models;

namespace RemoteNotes.Service.Client.Contract.Notes
{
    public interface INotesHub
    {
        event Action<IEnumerable<NoteModel>> NotesUpdated;
        
        Task<IEnumerable<NoteModel>> GetNotesAsync();

        Task PutNoteAsync(NoteModel note);
    }
}