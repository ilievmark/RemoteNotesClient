using System;
using System.IO;
using System.Text.RegularExpressions;
using RemoteNotes.Rules.Contract;

namespace RemoteNotes.Rules
{
    public class AuthorizationDataValidator : IAuthorizationDataValidator
    {
        public void ValidateLogin(string login)
        {
            if (string.IsNullOrEmpty(login))
                throw new ArgumentNullException(nameof(login));
            
            if (!Regex.IsMatch(login, RegexRules.Login))
                throw new InvalidDataException(nameof(login));
        }

        public void ValidatePassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password));
            
            if (!Regex.IsMatch(password, RegexRules.Password))
                throw new InvalidDataException(nameof(password));
        }
    }
}