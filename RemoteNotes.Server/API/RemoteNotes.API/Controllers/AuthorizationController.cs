using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RemoteNotes.BL.Authorization;
using RemoteNotes.Domain;
using RemoteNotes.Domain.Requests;
using RemoteNotes.Domain.Response;

namespace RemoteNotes.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : BaseApiController
    {
        private readonly AuthorizationService _authorizationService;

        public AuthorizationController(AuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }
        
        [HttpPost, Route("login")]
        public async Task<ActionResult> LoginAsync([FromBody] LoginRequest request)
        {
            var serverResult = new ApiResponse<AuthorizationResponse>();
            var tokenData = await _authorizationService.LogInAsync(request.Login, request.Password);

            serverResult.SetSuccess(new AuthorizationResponse(tokenData));

            return Ok(serverResult);
        }
    }
}