namespace RemoteNotes.Domain.Contract.Container
{
    public interface ITypeResolver
    {
        T Resolve<T>();

        T ResolveNamed<T>(string name);
    }
}
