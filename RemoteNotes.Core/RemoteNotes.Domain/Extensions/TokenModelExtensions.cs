using System;
using RemoteNotes.Domain.Models;

namespace RemoteNotes.Domain.Extensions
{
    public static class TokenModelExtensions
    {
        public static bool IsValid(this TokenModel tokenModel)
            => tokenModel != null &&
               tokenModel.Token != null &&
               tokenModel.Token.Length > 0 &&
               tokenModel.ExpireAt > DateTime.UtcNow;

        public static bool CanUpdate(this TokenModel tokenModel)
            => tokenModel != null &&
               tokenModel.Token != null &&
               tokenModel.Token.Length > 0 &&
               tokenModel.CanBeUpdatedTill > DateTime.UtcNow;
    }
}