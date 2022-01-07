using System;
using RemoteNotes.Domain.Models;

namespace RemoteNotes.Service.Client.Contract.User
{
    public interface IUserHub
    {
        event Action<UserModel> UserDataUpdated;
    }
}