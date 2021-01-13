namespace RemoteNotes.Domain
{
    public enum EResponseStatus
    {
        UndefinedError = -1,
        UnhandledError = 0,
        Success = 1,
        InvalidInputData = 2
    }
}