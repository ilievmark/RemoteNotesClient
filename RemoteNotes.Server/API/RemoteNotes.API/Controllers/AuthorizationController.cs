using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RemoteNotes.Domain;
using RemoteNotes.Domain.Requests;
using RemoteNotes.Domain.Response;

using IAuthorizationService = RemoteNotes.BL.Authorization.IAuthorizationService;

namespace RemoteNotes.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : BaseApiController
    {
        private readonly IAuthorizationService _authorizationService;

        protected string Token => Request.Headers["Authorization"];
        
        public AuthorizationController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }
        
        [HttpPost, Route("logIn")]
        public async Task<ActionResult> LogInAsync([FromBody] LoginRequest request)
        {
            var serverResult = new ApiResponse<AuthorizationResponse>();
            var tokenData = await _authorizationService.LogInAsync(request.Login, request.Password);

            serverResult.SetSuccess(new AuthorizationResponse(tokenData));

            return Ok(serverResult);
        }
        
        [HttpPost, Route("signUp")]
        public async Task<ActionResult> SignUpAsync([FromBody] SignUpRequest request)
        {
            var serverResult = new ApiResponse<AuthorizationResponse>();
            var tokenData = await _authorizationService.SignUpAsync(request.Username, request.Password);

            serverResult.SetSuccess(new AuthorizationResponse(tokenData));

            return Ok(serverResult);
        }
        
        [Authorize]
        [HttpPost, Route("refresh")]
        public async Task<ActionResult> RefreshAsync()
        {
            var serverResult = new ApiResponse<AuthorizationResponse>();
            var tokenData = await _authorizationService.RefreshTokenAsync(Token);

            serverResult.SetSuccess(new AuthorizationResponse(tokenData));

            return Ok(serverResult);
        }
    }
}