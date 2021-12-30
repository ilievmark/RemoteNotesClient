namespace RemoteNotes.Service.Client.Contract.Hub
{
    public interface IHubObservable
    {
        void Subscribe(IHubObserver observer);
        
        void Unsubscribe(IHubObserver observer);
    }
}