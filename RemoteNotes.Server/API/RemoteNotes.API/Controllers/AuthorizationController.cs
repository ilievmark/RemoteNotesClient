using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RemoteNotes.BL.Contract.UserToken;
using RemoteNotes.Domain;
using RemoteNotes.Domain.Endpoints;
using RemoteNotes.Domain.Requests;
using RemoteNotes.Domain.Response;

using IAuthorizationService = RemoteNotes.BL.Contract.Authorization.IAuthorizationService;

namespace RemoteNotes.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : BaseAuthorizedApiController
    {
        private readonly IAuthorizationService _authorizationService;

        public AuthorizationController(
            IUserTokenService userTokenService,
            IAuthorizationService authorizationService)
            : base(userTokenService)
        {
            _authorizationService = authorizationService;
        }
        
        [HttpPost, Route(AuthorizationApis.SignIn)]
        public async Task<ActionResult> LogInAsync([FromBody] SignInRequest request)
        {
            var serverResult = new ApiResponse<AuthorizationResponse>();
            var tokenData = await _authorizationService.SignInAsync(request.Username, request.Password);

            serverResult.SetSuccess(new AuthorizationResponse {TokenModel = tokenData});

            return Ok(serverResult);
        }
        
        [HttpPost, Route(AuthorizationApis.SignUp)]
        public async Task<ActionResult> SignUpAsync([FromBody] SignUpRequest request)
        {
            var serverResult = new ApiResponse<AuthorizationResponse>();
            var tokenData = await _authorizationService.SignUpAsync(request.Username, request.Password);

            serverResult.SetSuccess(new AuthorizationResponse {TokenModel = tokenData});

            return Ok(serverResult);
        }
        
        [Authorize]
        [HttpPost, Route(AuthorizationApis.RefreshToten)]
        public async Task<ActionResult> RefreshAsync()
        {
            var serverResult = new ApiResponse<AuthorizationResponse>();
            var tokenData = await _authorizationService.RefreshTokenAsync(Token);

            serverResult.SetSuccess(new AuthorizationResponse {TokenModel = tokenData});

            return Ok(serverResult);
        }
    }
}