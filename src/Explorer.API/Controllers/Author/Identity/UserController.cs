using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author.Identity
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/user/author/")]
    public class UserController : BaseApiController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("getUsername")]
        public ActionResult<string> getUsername([FromQuery] int userId)
        {
            var result = _userService.GetUsernameById(userId);
            return CreateResponse(result);
        }
    }
}
