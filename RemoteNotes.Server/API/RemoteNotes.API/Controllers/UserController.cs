using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RemoteNotes.BL.Contract.User;
using RemoteNotes.BL.Security.UserToken;
using RemoteNotes.Domain;
using RemoteNotes.Domain.Endpoints;
using RemoteNotes.Domain.Models;

namespace RemoteNotes.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseAuthorizedApiController
    {
        private readonly IUserService _userService;

        public UserController(
            IUserTokenService userTokenService,
            IUserService userService)
            : base(userTokenService)
        {
            _userService = userService;
        }
        
        [Authorize]
        [HttpGet, Route(UserApis.GetUser)]
        public async Task<ActionResult> GetProfileAsync()
        {
            var serverResult = new ApiResponse<UserModel>();
            var userModel = await _userService.GetUserProfileAsync(UserName);

            serverResult.SetSuccess(userModel);

            return Ok(serverResult);
        }
        
        [Authorize]
        [HttpPost, Route(UserApis.Update)]
        public async Task<ActionResult> UpdateUserDataAsync([FromBody] UserModel oldProfile)
        {
            var serverResult = new ApiResponse<UserModel>();
            var updatedProfile = await _userService.UpdateProfileAsync(oldProfile);

            serverResult.SetSuccess(updatedProfile);

            return Ok(serverResult);
        }
    }
}