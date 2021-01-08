using System;

namespace RemoteNotes.DAL.Models
{
    public class Note
    {
        public Note(NoteRead note)
        {
            Id = note.Id;
            Title = note.Title;
            Description = note.Description;
            CreatedAt = note.CreatedAt;
            ModifiedAt = note.ModifiedAt;
        }
        
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }
        
        public DateTime ModifiedAt { get; set; }
    }
}