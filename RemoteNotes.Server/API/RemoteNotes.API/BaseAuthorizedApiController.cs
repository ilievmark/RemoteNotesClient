using RemoteNotes.BL.Contract.UserToken;

namespace RemoteNotes.API
{
    public class BaseAuthorizedApiController : BaseApiController
    {
        private readonly IUserTokenService _userTokenService;

        public BaseAuthorizedApiController(IUserTokenService userTokenService)
        {
            _userTokenService = userTokenService;
        }
        
        protected string Token => Request.Headers["Authorization"];

        protected string UserName => _userTokenService.UserName(Token);
    }
}