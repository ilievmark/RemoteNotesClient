using System;

namespace RemoteNotes.Domain.Core.Navigation
{
    public class NavigationTypeRegistration
    {
        public Type Type { get; }

        public string Tag { get; }

        public NavigationTypeRegistration(Type type, string tag)
        {
            Type = type;
            Tag = tag;
        }
    }
}
