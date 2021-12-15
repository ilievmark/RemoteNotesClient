using System;
namespace RemoteNotes.Domain.Core.Attributes
{
    public class AuthorizedNavigationAttribute : Attribute
    {
        public string AlternativePageTag { get; set; }
    }
}
