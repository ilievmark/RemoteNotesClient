using System;

namespace RemoteNotes.Domain.Exceptions
{
    public class ServerUnhandledException : Exception
    {
        public ServerUnhandledException(string serverMessage)
            : base($"Server unhandled exception. Server message: {serverMessage}")
        {
            
        }
    }
}