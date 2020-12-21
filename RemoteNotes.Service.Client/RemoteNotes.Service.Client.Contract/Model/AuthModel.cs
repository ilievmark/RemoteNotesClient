using System;

namespace RemoteNotes.Service.Client.Contract.Model
{
    public class AuthModel
    {
        private AuthModel() {}
        
        public string Login { get; private set; }
        
        public string Password { get; private set; }

        public static AuthModel From(string login, string password)
        {
            var auth = new AuthModel();
            
            if (string.IsNullOrEmpty(login))
                throw new ArgumentNullException(nameof(login));
            auth.Login = login;
            
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password));
            auth.Password = password;

            return auth;
        }
    }
}