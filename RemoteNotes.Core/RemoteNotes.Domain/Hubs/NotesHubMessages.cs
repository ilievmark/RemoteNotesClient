namespace RemoteNotes.Domain.Hubs
{
    public class NotesHubMessages
    {
        public const string NotesUpdated = nameof(NotesUpdated);
        public const string GetNotes = nameof(GetNotes);
        public const string PutNote = nameof(PutNote);
        public const string DeleteNote = nameof(DeleteNote);
        public const string UpdateNote = nameof(UpdateNote);
    }
}