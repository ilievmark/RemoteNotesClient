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
            new NoteModel { Id = 0, Title = "Note 1", Description = "Description test appeared here for note 1" },
            new NoteModel { Id = 1, Title = "Note 2", Description = "Description test appeared here for note 2" },
            new NoteModel { Id = 2, Title = "Note 3", Description = "Description test appeared here for note 3" },
            new NoteModel { Id = 3, Title = "Note 4", Description = "Description test appeared here for note 4" },
            new NoteModel { Id = 4, Title = "Note 5", Description = "Description test appeared here for note 5" },
            new NoteModel { Id = 5, Title = "Note 6", Description = "Description test appeared here for note 6" },
        };
    }
}