namespace RemoteNotes.Domain.Entity
{
    public class User : IIdentificable
    {
        public int Id { get; set; }
        
        public string UserName { get; set; }
        
        public string Name { get; set; }
        
        public string Surname { get; set; }
        
        public string Email { get; set; }
        
        public string PhotoUrl { get; set; }
        
        public string EncryptedPassword { get; set; }
    }
}