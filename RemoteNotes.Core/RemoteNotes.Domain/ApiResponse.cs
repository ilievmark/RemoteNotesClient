namespace RemoteNotes.Domain
{
    public class ApiResponse
    {
        public bool IsSuccess { protected set; get; }
        
        public void SetSuccess()
        {
            IsSuccess = true;
        }
    }

    public class ApiResponse<T> : ApiResponse
    {
        public T Result { get; private set; }
        
        public void SetSuccess(T result)
        {
            SetSuccess();
            Result = result;
        }
    }
}
