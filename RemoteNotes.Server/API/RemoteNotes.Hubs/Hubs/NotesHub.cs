using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using RemoteNotes.BL.Note;
using RemoteNotes.BL.Security.UserToken;
using RemoteNotes.Domain.Entity;

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
        
        public Task<List<Note>> GetNotes()
        {
            var claims = Context.User.Claims;
            var userId = _userTokenService.UserId(claims);
            var guidUserId = Guid.Parse(userId);
            return _noteService.GetNotesAsync(guidUserId);
        }
    }
}