using RemoteNotes.Domain.Models;

namespace RemoteNotes.Domain.Hubs
{
    public class NoteChangeModel
    {
        public NoteChange Change { get; set; }
        
        public NoteModel Model { get; set; }
    }
}