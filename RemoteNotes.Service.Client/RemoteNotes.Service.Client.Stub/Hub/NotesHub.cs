using System.Collections.Generic;
using System.Threading.Tasks;
using RemoteNotes.Service.Client.Contract.Hub;
using RemoteNotes.Service.Client.Contract.Model;

namespace RemoteNotes.Service.Client.Stub.Hub
{
    public class NotesHub : IHubConnection, INotesHub
    {
        public bool IsConnected => true;
        
        public Task ConnectAsync() => Task.CompletedTask;

        public Task DisconnectAsync() => Task.CompletedTask;

        public async Task<IEnumerable<NoteModel>> GetNotesAsync()
        {
            await Task.Delay(2000);
            return StubConstants.StubNoteItems;
        }
    }
}