using System;

namespace RemoteNotes.Domain.Security
{
    public class TokenModel
    {
        public string Token { get; set; }
        public DateTimeOffset ExpireAt { get; set; }
    }
}