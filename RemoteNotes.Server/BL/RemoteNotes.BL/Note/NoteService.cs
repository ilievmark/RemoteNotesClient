using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RemoteNotes.BL.Contract.Note;
using RemoteNotes.DAL.Contract;
using RemoteNotes.Domain.Models;

namespace RemoteNotes.BL.Note
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _noteRepository;

        public NoteService(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public Task SaveNoteAsync(int userId, NoteModel note)
        {
            var entity = ToEntity(note);
            entity.UserId = userId;
            _noteRepository.Add(entity);
            return Task.CompletedTask;
        }

        public Task DeleteNoteAsync(NoteModel note)
        {
            _noteRepository.Delete(note.Id);
            return Task.CompletedTask;
        }

        public Task<NoteModel> UpdateNoteAsync(NoteModel note)
        {
            var n = _noteRepository.GetById(note.Id);

            n.Description = note.Text;
            n.Title = note.Topic;
            n.ModifiedAt = DateTime.Now;
            n.PhotoUrl = note.PhotoUrl;
            
            _noteRepository.Update(n);

            return Task.FromResult(note);
        }

        public async Task<List<NoteModel>> GetNotesAsync(int userId)
        {
            var notes = await _noteRepository.GetByUserIdAsync(userId);
            return notes.Select(ToModel).ToList();
        }

        private NoteModel ToModel(Domain.Entity.Note note)
        {
            return new NoteModel
            {
                Id = note.Id,
                AuthorUserId = note.UserId,
                LastModifyTime = note.ModifiedAt,
                PhotoUrl = note.PhotoUrl,
                PublishTime = note.CreatedAt,
                Text = note.Description,
                Topic = note.Title
            };
        }

        private Domain.Entity.Note ToEntity(NoteModel note)
        {
            return new Domain.Entity.Note
            {
                Id = note.Id,
                UserId = note.AuthorUserId,
                PhotoUrl = note.PhotoUrl,
                CreatedAt = note.PublishTime,
                Description = note.Text,
                Title = note.Topic
            };
        }
    }
}