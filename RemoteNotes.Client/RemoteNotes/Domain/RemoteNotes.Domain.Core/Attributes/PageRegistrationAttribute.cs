using System;

namespace RemoteNotes.Domain.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class PageRegistrationAttribute : NavigationAttribute
    {
    }
}
