using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RemoteNotes.Domain.Hubs;
using RemoteNotes.Domain.Models;

namespace RemoteNotes.Service.Client.Contract.Notes
{
    public interface INotesHub
    {
        event Action<NoteChange, NoteModel> NoteStorageUpdate;
        
        Task<IEnumerable<NoteModel>> GetNotesAsync();

        Task DeleteNoteAsync(NoteModel note);
        
        Task UpdateNoteAsync(NoteModel note);
        
        Task PutNoteAsync(NoteModel note);
    }
}