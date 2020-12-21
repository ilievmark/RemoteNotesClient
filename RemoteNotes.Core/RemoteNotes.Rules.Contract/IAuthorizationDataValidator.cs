namespace RemoteNotes.Rules.Contract
{
    public interface IAuthorizationDataValidator
    {
        void ValidateLogin(string login);
        
        void ValidatePassword(string password);
    }
}