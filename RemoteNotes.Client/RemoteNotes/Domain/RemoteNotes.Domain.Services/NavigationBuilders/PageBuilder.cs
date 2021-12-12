using RemoteNotes.Domain.Contract.Container;
using RemoteNotes.Domain.Contract.Navigation;
using Xamarin.Forms;

namespace RemoteNotes.Domain.Services.NavigationBuilders
{
    public class PageBuilder : IPageBuilder
    {
        private readonly ITypeResolver _typeResolver;

        public PageBuilder(ITypeResolver typeResolver)
        {
            _typeResolver = typeResolver;
        }

        public Page BuildPage(string tag)
        {
            return _typeResolver.ResolveNamed<Page>(tag);
        }
    }
}
