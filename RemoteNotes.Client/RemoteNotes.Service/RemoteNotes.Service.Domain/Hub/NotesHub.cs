using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using RemoteNotes.Service.Client.Contract.Authorization;
using RemoteNotes.Service.Client.Contract.Hub;
using RemoteNotes.Service.Client.Contract.Model;

namespace RemoteNotes.Service.Domain.Hub
{
    public class NotesHub : BaseHub, INotesHub
    {
        private readonly IAuthorizationHolder _authorizationHolder;

        private string HubUrl => Constants.BaseUrl + "/notes";

        public NotesHub(IAuthorizationHolder authorizationHolder)
        {
            _authorizationHolder = authorizationHolder;
        }

        protected override HubConnection BuildConnection()
        {
            return new HubConnectionBuilder()
                .WithUrl(
                    HubUrl,
                    options => options.AccessTokenProvider = GetTokenAsync)
                .Build();
        }

        protected override void SubscribeEvents(HubConnection hubConnection)
        {
        }

        private async Task<string> GetTokenAsync()
            => _authorizationHolder.GetData()?.TokenModel?.Token;

        public Task<IEnumerable<NoteModel>> GetNotesAsync()
        {
            if (!IsConnected)
                ConnectAsync();

            return _hubConnection.InvokeCoreAsync<IEnumerable<NoteModel>>("GetNotes", new object[] { });
        }
    }
}