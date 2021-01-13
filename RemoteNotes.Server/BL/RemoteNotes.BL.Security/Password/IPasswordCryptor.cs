namespace RemoteNotes.BL.Security.Password
{
    public interface IPasswordCryptor
    {
        bool IsPasswordsEquals(string cryptedPassword, string rawPassword);

        string ToPassword(string password);
    }
}