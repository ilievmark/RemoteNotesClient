using System;
using RemoteNotes.DAL.Contract;
using RemoteNotes.DAL.IntegrationTest.Base;
using RemoteNotes.Domain.Entity;

namespace RemoteNotes.DAL.IntegrationTest
{
    public class NoteRepositoryTester : RepositoryTesterBase<INoteRepository, Note>
    {
        protected override void ModifyProperties(Note note)
        {
            note.Title = "Changed";
            note.Description = "Something";
        }

        protected override Note BuildObject()
        {
            return new Note
            {
                Id = Guid.NewGuid(),
                Title = "Topic",
                Description = "Text with description",
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            };
        }
    }
}
