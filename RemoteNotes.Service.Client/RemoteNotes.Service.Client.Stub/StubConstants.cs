using System.Collections.Generic;
using RemoteNotes.Service.Client.Contract.Model;

namespace RemoteNotes.Service.Client.Stub
{
    public static class StubConstants
    {
        public const string Login = "Silvester_222";
        public const string Password = "Password222";

        public static IReadOnlyList<NoteModel> StubNoteItems = new List<NoteModel>()
        {
            new NoteModel(),
            new NoteModel(),
            new NoteModel(),
            new NoteModel(),
            new NoteModel()
        };
    }
}