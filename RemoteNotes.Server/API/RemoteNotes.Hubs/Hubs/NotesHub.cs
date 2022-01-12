using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using RemoteNotes.BL.Contract.Note;
using RemoteNotes.BL.Security.UserToken;
using RemoteNotes.Domain.Hubs;
using RemoteNotes.Domain.Models;

namespace RemoteNotes.Hubs.Hubs
{
    [Authorize]
    public class NotesHub : Hub
    {
        private readonly IUserTokenService _userTokenService;
        private readonly INoteService _noteService;

        public NotesHub(
            IUserTokenService userTokenService,
            INoteService noteService)
        {
            _userTokenService = userTokenService;
            _noteService = noteService;
        }
        
        public Task<List<NoteModel>> GetNotes()
        {
            var claims = Context.User.Claims;
            var userId = _userTokenService.UserId(claims);
            return _noteService.GetNotesAsync(userId);
        }

        public async Task PutNote(NoteModel note)
        {
            var claims = Context.User.Claims;
            var userId = _userTokenService.UserId(claims);
            await _noteService.SaveNoteAsync(userId, note);
            var noteChange = new NoteChangeModel { Change = NoteChange.Added, Model = note };
            await Clients.Caller.SendCoreAsync(NotesHubMessages.NotesUpdated, new [] { noteChange });
        }

        public async Task<NoteModel> UpdateNote(NoteModel note)
        {
            await _noteService.UpdateNoteAsync(note);
            var noteChange = new NoteChangeModel { Change = NoteChange.Updated, Model = note };
            await Clients.Caller.SendCoreAsync(NotesHubMessages.NotesUpdated, new [] { noteChange });
            return note;
        }

        public async Task DeleteNote(NoteModel note)
        {
            await _noteService.DeleteNoteAsync(note);
            var noteChange = new NoteChangeModel { Change = NoteChange.Deleted, Model = note };
            await Clients.Caller.SendCoreAsync(NotesHubMessages.NotesUpdated, new [] { noteChange });
        }
    }
}