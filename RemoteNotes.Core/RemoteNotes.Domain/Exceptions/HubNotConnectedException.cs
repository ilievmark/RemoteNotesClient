using System;

namespace RemoteNotes.Domain.Exceptions
{
    public class HubNotConnectedException : ApplicationException
    {
        public HubNotConnectedException(string name) : base(name)
        {
        }
    }
}