using Microsoft.AspNetCore.SignalR;
using RemoteNotes.BL.Contract.User;
using RemoteNotes.BL.Contract.UserToken;
using RemoteNotes.Domain.Hubs;
using RemoteNotes.Domain.Models;

namespace RemoteNotes.Hubs.Hubs
{
    public class UserHub : Hub
    {
        private readonly IUserTokenService _userTokenService;

        public UserHub(
            IUserTokenService userTokenService,
            IUserService userService)
        {
            _userTokenService = userTokenService;
            
            userService.UserDataChanged += OnUserDataChanged;
        }

        private async void OnUserDataChanged(int userId, UserModel userData)
        {
            var claims = Context.User.Claims;
            var userIdFromToken = _userTokenService.UserId(claims);

            if (userIdFromToken == userId)
            {
                await Clients.Caller.SendCoreAsync(UserHubMessages.ProfileUpdated, new [] { userData });
            }
        }
    }
}