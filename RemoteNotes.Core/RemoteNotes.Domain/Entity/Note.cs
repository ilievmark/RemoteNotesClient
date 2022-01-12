using System;

namespace RemoteNotes.Domain.Entity
{
    public class Note : IModificable, IIdentificable
    {
        public int Id { get; set; }
        
        public int UserId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string PhotoUrl { get; set; }

        public DateTime CreatedAt { get; set; }
        
        public DateTime ModifiedAt { get; set; }
    }
}