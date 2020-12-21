namespace RemoteNotes.Service.Client.Contract.Model
{
    public class NoteModel : INote
    {
        public int Id { get; set; }
        
        public string Title { get; set; }
        
        public string Description { get; set; }
    }
}