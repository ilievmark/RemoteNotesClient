using System;

namespace RemoteNotes.Domain.Entity
{
    public interface IIdentificable
    {
        public Guid Id { get; set; }
    }
}