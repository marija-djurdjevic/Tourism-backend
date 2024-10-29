using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.Identity
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/user/tourist/")]
    public class UserController : BaseApiController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("/getLocation")]
        public ActionResult<UserProfileDto> Get()
        {
            var userId = User.FindFirst("id")?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var result = _userService.GetUserLocation(Int32.Parse(userId));
            return CreateResponse(result);
        }

        [HttpPut("/setLocation")]
        public ActionResult<UserProfileDto> SetTouristLocation([FromQuery] float longitude, [FromQuery] float latitude)
        {
            var userId = User.FindFirst("id")?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var result = _userService.SetUserLocation(int.Parse(userId), longitude, latitude);
            return CreateResponse(result);
        }

    }
}
