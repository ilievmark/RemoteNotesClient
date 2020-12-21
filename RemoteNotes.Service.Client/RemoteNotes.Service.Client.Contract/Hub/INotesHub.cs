using System.Collections.Generic;
using System.Threading.Tasks;
using RemoteNotes.Service.Client.Contract.Model;

namespace RemoteNotes.Service.Client.Contract.Hub
{
    public interface INotesHub
    {
        Task<IEnumerable<NoteModel>> GetNotesAsync();
    }
}