using System;

namespace RemoteNotes.Domain.Entity
{
    public interface IModificable
    {
        public DateTime CreatedAt { get; set; }
        
        public DateTime ModifiedAt { get; set; }
    }
}