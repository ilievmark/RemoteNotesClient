using System;
using System.Threading.Tasks;
using RemoteNotes.Domain.Extensions;
using RemoteNotes.Domain.Hubs;
using RemoteNotes.Domain.Models;
using RemoteNotes.Service.Client.Contract.Hub;
using RemoteNotes.Service.Client.Contract.User;

namespace RemoteNotes.Service.Domain.User
{
    public class UserHub : IUserHub, IHubObserver
    {
        public event Action<UserModel> UserDataUpdated = delegate { };
        
        public string HubName => Hubs.User;

        public string MessageTag => UserHubMessages.ProfileUpdated;
        
        public Task HandleMessageAsync(string json)
        {
            var user = json.ParseAsJson<UserModel>();
            UserDataUpdated?.Invoke(user);
            return Task.CompletedTask;
        }
    }
}