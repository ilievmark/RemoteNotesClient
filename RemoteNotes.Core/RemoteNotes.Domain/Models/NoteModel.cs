using System;

namespace RemoteNotes.Domain.Models
{
    public class NoteModel
    {
        public int Id { get; set; }
        
        public int AuthorUserId { get; set; }

        public string Topic { get; set; }
        
        public string Text { get; set; }
        
        public string PhotoUrl { get; set; }
        
        public DateTime PublishTime { get; set; }
        
        public DateTime? LastModifyTime { get; set; }
    }
}