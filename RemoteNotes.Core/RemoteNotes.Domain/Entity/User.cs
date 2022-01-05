namespace RemoteNotes.Domain.Entity
{
    public class User : IIdentificable
    {
        public int Id { get; set; }
        
        public string Username { get; set; }

        public string EncryptedPassword { get; set; }
    }
}