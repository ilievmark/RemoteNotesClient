namespace RemoteNotes.Domain
{
    public class ApiResponse
    {
        public EResponseStatus Status { set; get; }
        
        public string Message { set; get; }
        
        public void SetSuccess()
        {
            Status = EResponseStatus.Success;
        }

        public void SetOtherResult(EResponseStatus status, string message = null)
        {
            Status = status;
            Message = message;
        }
    }

    public class ApiResponse<T> : ApiResponse
    {
        public T Result { get; set; }
        
        public void SetSuccess(T result)
        {
            SetSuccess();
            Result = result;
        }
    }
}
