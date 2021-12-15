using System;
using Autofac;
using RemoteNotes.Domain.Contract.Container;

namespace RemoteNotes.Domain.Services.Container
{
    public class TypeResolver : ITypeResolver
    {
        private Func<IContainer> _container;

        public TypeResolver(
            Func<IContainer> container)
        {
            _container = container;
        }

        public T Resolve<T>()
        {
            return _container().Resolve<T>();
        }

        public T ResolveNamed<T>(string name)
        {
            return _container().ResolveNamed<T>(name);
        }
    }
}
