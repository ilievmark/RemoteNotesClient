namespace RemoteNotes.Domain.Contract.Navigation
{
    public interface IViewModelBuilder
    {
        object BuildViewModel(string tag);
    }
}
