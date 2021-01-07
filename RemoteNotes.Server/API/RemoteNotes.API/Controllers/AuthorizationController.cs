using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RemoteNotes.API.Requests;
using RemoteNotes.Domain;

namespace RemoteNotes.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : BaseApiController
    {
        [HttpPost, Route("login")]
        public async Task<ActionResult> LoginAsync([FromBody] LoginRequest request)
        {
            var serverResult = new ApiResponse();
            
            

            return Ok(serverResult);
        }
    }
}