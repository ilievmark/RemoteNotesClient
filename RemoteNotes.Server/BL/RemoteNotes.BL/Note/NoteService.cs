using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        
        public async Task<IEnumerable<DAL.Models.Note>> GetNotesAsync(Guid userId)
        {
            var allUserNotes = await _noteRepository.GetByUserIdAsync(userId);
            return allUserNotes.Select(n => new DAL.Models.Note(n));
        }
    }
}