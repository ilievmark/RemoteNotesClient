using System.Linq;
using RemoteNotes.Domain.Core.Attributes;
using RemoteNotes.Domain.Core.Navigation;

namespace RemoteNotes.Domain.Core.Extensions
{
    public static class NavigationTypeRegistrationExtensions
    {
        public static bool IsAuthorized(this NavigationTypeRegistration registration)
            => registration.GetAuthorizedAttributeData() != null;

        public static string GetAlternativeNavigationTag(this NavigationTypeRegistration registration)
            => registration.GetAuthorizedAttributeData()?.AlternativePageTag;

        private static AuthorizedNavigationAttribute GetAuthorizedAttributeData(this NavigationTypeRegistration registration)
            => registration.Type.GetCustomAttributes(typeof(AuthorizedNavigationAttribute), false).FirstOrDefault() as AuthorizedNavigationAttribute;
    }
}
