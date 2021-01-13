using System;

namespace RemoteNotes.Domain.Entity
{
    public class Note : IModificable, IIdentificable
    {
        public Guid Id { get; set; }
        
        public Guid UserId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }
        
        public DateTime ModifiedAt { get; set; }
    }
}