using System;

namespace RemoteNotes.Domain.Core.Attributes
{
    public abstract class NavigationAttribute : Attribute
    {
        public string NavigationTag { get; set; }
    }
}
