using System;
using System.Collections.Generic;
using System.Linq;
using RemoteNotes.Domain.Core.Attributes;
using RemoteNotes.Domain.Core.Navigation;

namespace RemoteNotes.Domain.Services.NavigationBuilders.Registrators
{
    public class RegisteredNavigationTypesProvider
    {
        public IEnumerable<NavigationTypeRegistration> GetRegistrations<TAttribute>(string assembly)
            where TAttribute : NavigationAttribute
        {
            var navigationTypes = AppDomain.CurrentDomain
                .GetAssemblies()
                .First(a => a.FullName.Contains(assembly))
                .GetTypes()
                .Where(t => t.CustomAttributes.Any(a => a.AttributeType == typeof(TAttribute)))
                .ToList();

            foreach (var type in navigationTypes)
            {
                var attributes = type.GetCustomAttributes(typeof(TAttribute), false);
                foreach (var attribute in attributes)
                    if (attribute is TAttribute casted)
                        yield return new NavigationTypeRegistration(type, casted.NavigationTag);
            }
        }
    }
}
