using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RemoteNotes.BL.Contract.Note;
using RemoteNotes.Domain.Models;

namespace RemoteNotes.BL.Stub.Note
{
    public class NoteService : INoteService
    {
        private List<NoteModel> _notes;
        
        public NoteService()
        {
            _notes = new List<NoteModel>
            {
                new NoteModel {Id = 0, AuthorUserId = 0, PhotoUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/5a/Books_HD_%288314929977%29.jpg/1280px-Books_HD_%288314929977%29.jpg", Text = "Note 1", Topic = "Topic 1", PublishTime = DateTime.Now.AddDays(-1).AddHours(-3).AddMinutes(-23), LastModifyTime = null},
                new NoteModel {Id = 1, AuthorUserId = 0, PhotoUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/5a/Books_HD_%288314929977%29.jpg/1280px-Books_HD_%288314929977%29.jpg", Text = "Note 2", Topic = "Topic 1", PublishTime = DateTime.Now, LastModifyTime = DateTime.Now},
                new NoteModel {Id = 2, AuthorUserId = 1, PhotoUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/5a/Books_HD_%288314929977%29.jpg/1280px-Books_HD_%288314929977%29.jpg", Text = "Note 3", Topic = "Topic 2", PublishTime = DateTime.Now, LastModifyTime = null},
                new NoteModel {Id = 3, AuthorUserId = 0, PhotoUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/5a/Books_HD_%288314929977%29.jpg/1280px-Books_HD_%288314929977%29.jpg", Text = "Note 4", Topic = "Topic 1", PublishTime = DateTime.Now, LastModifyTime = null},
                new NoteModel {Id = 4, AuthorUserId = 0, PhotoUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/5a/Books_HD_%288314929977%29.jpg/1280px-Books_HD_%288314929977%29.jpg", Text = "Note 5", Topic = "Topic 1", PublishTime = DateTime.Now, LastModifyTime = null},
                new NoteModel {Id = 5, AuthorUserId = 0, PhotoUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/5a/Books_HD_%288314929977%29.jpg/1280px-Books_HD_%288314929977%29.jpg", Text = "Note 6", Topic = "Topic 2", PublishTime = DateTime.Now, LastModifyTime = null},
                new NoteModel {Id = 6, AuthorUserId = 2, PhotoUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/5a/Books_HD_%288314929977%29.jpg/1280px-Books_HD_%288314929977%29.jpg", Text = "Note 7", Topic = "Topic 1", PublishTime = DateTime.Now, LastModifyTime = null},
                new NoteModel {Id = 7, AuthorUserId = 2, PhotoUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/5a/Books_HD_%288314929977%29.jpg/1280px-Books_HD_%288314929977%29.jpg", Text = "Note 8", Topic = "Topic 1", PublishTime = DateTime.Now, LastModifyTime = null},
                new NoteModel {Id = 8, AuthorUserId = 1, PhotoUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/5a/Books_HD_%288314929977%29.jpg/1280px-Books_HD_%288314929977%29.jpg", Text = "Note 9", Topic = "Topic 3", PublishTime = DateTime.Now, LastModifyTime = null},
                new NoteModel {Id = 9, AuthorUserId = 0, PhotoUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/5a/Books_HD_%288314929977%29.jpg/1280px-Books_HD_%288314929977%29.jpg", Text = "Note 10", Topic = "Topic 4", PublishTime = DateTime.Now, LastModifyTime = null},
                new NoteModel {Id = 10, AuthorUserId = 0, PhotoUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/5a/Books_HD_%288314929977%29.jpg/1280px-Books_HD_%288314929977%29.jpg", Text = "Note 11", Topic = "Topic 4", PublishTime = DateTime.Now, LastModifyTime = null},
            };
        }
        
        public async Task<List<NoteModel>> GetNotesAsync(int userId)
        {
            await Task.Delay(2000);
            return _notes;
        }

        public async Task SaveNoteAsync(int userId, NoteModel note)
        {
            await Task.Delay(2000);
            note.Id = _notes.Count;
            _notes.Add(note);
        }

        public async Task DeleteNoteAsync(NoteModel note)
        {
            await Task.Delay(2000);
            var n = _notes.Find(no => no.Id == note.Id);
            _notes.Remove(n);
        }

        public async Task<NoteModel> UpdateNoteAsync(NoteModel note)
        {
            await Task.Delay(2000);
            var n = _notes.Find(no => no.Id == note.Id);
            _notes.Remove(n);
            _notes.Add(note);
            return note;
        }
    }
}