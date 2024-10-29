using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Tours.API.Dtos;
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

        [HttpGet("getLocation")]
        public ActionResult<LocationDto> Get()
        {
            var userId = User.FindFirst("id")?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var result = _userService.GetUserLocation(Int32.Parse(userId));
            return CreateResponse(result);
        }

        [HttpPost("setLocation")]
        public ActionResult<LocationDto> SetTouristLocation([FromBody] LocationDto location)
        {
            var userId = User.FindFirst("id")?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var result = _userService.SetUserLocation(int.Parse(userId), location.Longitude, location.Latitude);
            return CreateResponse(result);
        }

    }
}
