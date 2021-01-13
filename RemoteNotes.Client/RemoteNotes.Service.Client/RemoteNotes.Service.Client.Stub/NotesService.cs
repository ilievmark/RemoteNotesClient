using System.Collections.Generic;
using System.Threading.Tasks;
using RemoteNotes.Service.Client.Contract;
using RemoteNotes.Service.Client.Contract.Hub;
using RemoteNotes.Service.Client.Contract.Model;

namespace RemoteNotes.Service.Client.Stub
{
    public class NotesService : INotesService
    {
        private readonly INotesHub _notesHub;

        public NotesService(INotesHub notesHub)
        {
            _notesHub = notesHub;
        }

        public Task<IEnumerable<NoteModel>> GetNotesAsync()
            => _notesHub.GetNotesAsync();
    }
}