namespace RemoteNotes.Domain.Extensions
{
    public static class EResponseStatusExtensions
    {
        public static bool IsSuccess(this EResponseStatus status)
            => status == EResponseStatus.Success;

        public static bool IsFailure(this EResponseStatus status)
            => !status.IsSuccess();
    }
}