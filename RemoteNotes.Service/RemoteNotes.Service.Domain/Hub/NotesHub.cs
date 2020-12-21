using System.Collections.Generic;
using System.Threading.Tasks;
using RemoteNotes.Service.Client.Contract.Hub;
using RemoteNotes.Service.Client.Contract.Model;

namespace RemoteNotes.Service.Domain.Hub
{
    public class NotesHub : BaseHub, IHubConnection, INotesHub
    {
        public bool IsConnected { get; }
        public Task ConnectAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task DisconnectAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<NoteModel>> GetNotesAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}