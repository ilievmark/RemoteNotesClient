using System.Collections.Generic;
using System.Threading.Tasks;
using RemoteNotes.BL.Contract.Note;
using RemoteNotes.DAL.Contract;

namespace RemoteNotes.BL.Note
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _noteRepository;

        public NoteService(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public Task<List<Domain.Entity.Note>> GetNotesAsync(int userId)
            => _noteRepository.GetByUserIdAsync(userId);
    }
}