using System.Collections.Generic;
using System.Threading.Tasks;
using RemoteNotes.Service.Client.Contract.Model;

namespace RemoteNotes.Service.Client.Contract
{
    public interface INotesService
    {
        Task<IEnumerable<NoteModel>> GetNotesAsync();
    }
}