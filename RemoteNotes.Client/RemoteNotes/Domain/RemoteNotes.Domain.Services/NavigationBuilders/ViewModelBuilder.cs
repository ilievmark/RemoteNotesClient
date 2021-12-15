using RemoteNotes.Domain.Contract.Container;
using RemoteNotes.Domain.Contract.Navigation;
using RemoteNotes.Domain.Services.ViewModel;

namespace RemoteNotes.Domain.Services.NavigationBuilders
{
    public class ViewModelBuilder : IViewModelBuilder
    {
        private readonly ITypeResolver _typeResolver;

        public ViewModelBuilder(ITypeResolver typeResolver)
        {
            _typeResolver = typeResolver;
        }

        public object BuildViewModel(string tag)
        {
            return _typeResolver.ResolveNamed<BaseViewModel>(tag);
        }
    }
}
