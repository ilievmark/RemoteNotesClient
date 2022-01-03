namespace RemoteNotes.API
{
    public class BaseAuthorizedApiController : BaseApiController
    {
        protected string Token => Request.Headers["Authorization"];
    }
}