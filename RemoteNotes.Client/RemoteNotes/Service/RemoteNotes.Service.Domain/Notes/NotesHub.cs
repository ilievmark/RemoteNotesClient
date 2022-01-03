using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RemoteNotes.Domain.Extensions;
using RemoteNotes.Domain.Hubs;
using RemoteNotes.Domain.Models;
using RemoteNotes.Service.Client.Contract.Hub;
using RemoteNotes.Service.Client.Contract.Notes;

namespace RemoteNotes.Service.Domain.Notes
{
    public class NotesHub : INotesHub, IHubObserver
    {
        private readonly IHubMessager _hubMessager;
        
        public event Action<NoteChange, NoteModel> NoteStorageUpdate = delegate { };

        public string HubName => Hubs.Notes;

        public string MessageTag => NotesHubMessages.NotesUpdated;
        
        public NotesHub(IHubMessager hubMessager)
        {
            _hubMessager = hubMessager;
        }
        
        public Task<IEnumerable<NoteModel>> GetNotesAsync()
        {
            return _hubMessager.SendMessageAsync<string, IEnumerable<NoteModel>>(NotesHubMessages.GetNotes, null);
        }

        public Task DeleteNoteAsync(NoteModel note)
        {
            throw new NotImplementedException();
        }

        public Task UpdateNoteAsync(NoteModel note)
        {
            throw new NotImplementedException();
        }

        public Task PutNoteAsync(NoteModel note)
        {
            return _hubMessager.SendMessageAsync<NoteModel, string>(NotesHubMessages.PutNote, note);
        }

        public Task HandleMessageAsync(string json)
        {
            var notes = json.ParseAsJson<NoteChangeModel>();
            NoteStorageUpdate?.Invoke(notes.Change, notes.Model);
            return Task.CompletedTask;
        }
    }
}