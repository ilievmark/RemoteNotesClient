namespace RemoteNotes.Service.Client.Contract.Model
{
    public interface INote
    {
        public int Id { get; }
        
        public string Title { get; }
        
        public string Description { get; }
    }
}