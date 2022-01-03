using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RemoteNotes.Domain.Hubs;
using RemoteNotes.Domain.Models;
using RemoteNotes.Service.Client.Contract.Hub;
using RemoteNotes.Service.Client.Contract.Notes;

namespace RemoteNotes.Service.Domain.Stub.Notes
{
    public class NotesHub : INotesHub, IHubObserver
    {
        private readonly IHubMessager _hubMessager;
        private IList<NoteModel> _notesStorage;

        public event Action<NoteChange, NoteModel> NoteStorageUpdate = delegate {};
        
        public string HubName => Hubs.Notes;

        public string MessageTag => NotesHubMessages.NotesUpdated;

        public NotesHub(IHubMessager hubMessager)
        {
            _hubMessager = hubMessager;
            _notesStorage = new List<NoteModel>();
            InitNotes();
        }

        private void InitNotes()
        {
            _notesStorage = new List<NoteModel>
            {
                new NoteModel {Id = 0, AuthorUserId = 0, PhotoUrl = "", Text = "Note 1", Topic = "Topic 1", PublishTime = DateTime.Now, LastModifyTime = null},
                new NoteModel {Id = 1, AuthorUserId = 0, PhotoUrl = "", Text = "Note 2", Topic = "Topic 1", PublishTime = DateTime.Now, LastModifyTime = null},
                new NoteModel {Id = 2, AuthorUserId = 1, PhotoUrl = "", Text = "Note 3", Topic = "Topic 2", PublishTime = DateTime.Now, LastModifyTime = null},
                new NoteModel {Id = 3, AuthorUserId = 0, PhotoUrl = "", Text = "Note 4", Topic = "Topic 1", PublishTime = DateTime.Now, LastModifyTime = null},
                new NoteModel {Id = 4, AuthorUserId = 0, PhotoUrl = "", Text = "Note 5", Topic = "Topic 1", PublishTime = DateTime.Now, LastModifyTime = null},
                new NoteModel {Id = 5, AuthorUserId = 0, PhotoUrl = "", Text = "Note 6", Topic = "Topic 2", PublishTime = DateTime.Now, LastModifyTime = null},
                new NoteModel {Id = 6, AuthorUserId = 2, PhotoUrl = "", Text = "Note 7", Topic = "Topic 1", PublishTime = DateTime.Now, LastModifyTime = null},
                new NoteModel {Id = 7, AuthorUserId = 2, PhotoUrl = "", Text = "Note 8", Topic = "Topic 1", PublishTime = DateTime.Now, LastModifyTime = null},
                new NoteModel {Id = 8, AuthorUserId = 1, PhotoUrl = "", Text = "Note 9", Topic = "Topic 3", PublishTime = DateTime.Now, LastModifyTime = null},
                new NoteModel {Id = 9, AuthorUserId = 0, PhotoUrl = "", Text = "Note 10", Topic = "Topic 4", PublishTime = DateTime.Now, LastModifyTime = null},
                new NoteModel {Id = 10, AuthorUserId = 0, PhotoUrl = "", Text = "Note 11", Topic = "Topic 4", PublishTime = DateTime.Now, LastModifyTime = null},
            };
        }

        public async Task<IEnumerable<NoteModel>> GetNotesAsync()
        {
            await Task.Delay(2000);
            return _notesStorage;
        }

        public async Task DeleteNoteAsync(NoteModel note)
        {
            await Task.Delay(2000);
            var oldNote = _notesStorage.FirstOrDefault(n => n.Id == note.Id);
            var index = _notesStorage.IndexOf(oldNote);
            _notesStorage.RemoveAt(index);
            NoteStorageUpdate(NoteChange.Deleted, note);
        }

        public async Task UpdateNoteAsync(NoteModel note)
        {
            await Task.Delay(2000);
            var oldNote = _notesStorage.FirstOrDefault(n => n.Id == note.Id);
            var index = _notesStorage.IndexOf(oldNote);
            _notesStorage[index] = note;
            NoteStorageUpdate(NoteChange.Updated, note);
        }

        public async Task PutNoteAsync(NoteModel note)
        {
            await Task.Delay(2000);
            note.Id = _notesStorage.Last().Id + 1;
            _notesStorage.Add(note);
            NoteStorageUpdate(NoteChange.Added, note);
        }

        public Task HandleMessageAsync(string json)
        {
            return Task.CompletedTask;
        }
    }
}