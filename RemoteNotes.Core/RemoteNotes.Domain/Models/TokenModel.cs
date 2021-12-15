using System;

namespace RemoteNotes.Domain.Models
{
    public class TokenModel
    {
        public string Token { get; set; }
        
        public DateTimeOffset ExpireAt { get; set; }
        
        public DateTimeOffset CanBeUpdatedTill { get; set; }
    }
}