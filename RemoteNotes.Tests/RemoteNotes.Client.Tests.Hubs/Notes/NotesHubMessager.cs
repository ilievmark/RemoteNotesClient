using System.Collections.Generic;
using System.Threading.Tasks;
using RemoteNotes.Domain.Hubs;
using RemoteNotes.Domain.Models;
using RemoteNotes.Service.Client.Contract.Hub;

namespace RemoteNotes.Client.Tests.Hubs.Notes
{
    public class NotesHubMessager : IHubMessager
    {
        private IList<NoteModel> _notes;

        public NotesHubMessager()
        {
            _notes = new List<NoteModel>
            {
                new NoteModel {Id = 0},
                new NoteModel {Id = 1},
            };
        }
        
        public async Task<TResult> SendMessageAsync<TParam, TResult>(string messageTag, TParam param)
        {
            if (messageTag == NotesHubMessages.GetNotes)
                return (TResult)_notes;

            if (messageTag == NotesHubMessages.PutNote)
                _notes.Add(param as NoteModel);

            if (messageTag == NotesHubMessages.DeleteNote)
                _notes.Remove(param as NoteModel);

            return default(TResult);
        }
    }
}