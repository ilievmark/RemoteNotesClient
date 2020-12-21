namespace RemoteNotes.Service.Client.Contract.Model
{
    public class AuthResult
    {
        public AuthResult(bool success)
        {
            Success = success;
        }
        
        public bool Success { get; private set; }
    }
}