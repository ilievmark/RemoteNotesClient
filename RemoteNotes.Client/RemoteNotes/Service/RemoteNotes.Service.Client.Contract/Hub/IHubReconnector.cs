namespace RemoteNotes.Service.Client.Contract.Hub
{
    public interface IHubReconnector
    {
        void StartObserveForReconnection(IHubConnection hubConnection);
        
        void StopObserveForReconnection(IHubConnection hubConnection);
    }
}