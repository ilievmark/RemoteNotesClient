using RemoteNotes.Service.Domain.Notes;

namespace RemoteNotes.Client.Tests.Hubs.Notes
{
    public static class NotesHubFactory
    {
        public static NotesHub GetHub()
        {
            return new NotesHub(
                new NotesHubMessager());
        }
    }
}