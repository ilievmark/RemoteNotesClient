namespace RemoteNotes.BL.Security.Password
{
    public class PasswordCryptor : IPasswordCryptor
    {
        public bool IsPasswordsEquals(string cryptedPassword, string rawPassword)
            => BCrypt.Net.BCrypt.Verify(rawPassword, cryptedPassword);

        public string ToPassword(string income)
            => BCrypt.Net.BCrypt.HashPassword(income);
    }
}