using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Infrastructure.Authentication;
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
            var result = _userService.GetUserLocation(User.PersonId());
            return CreateResponse(result);
        }

        [HttpPost("setLocation")]
        public ActionResult<LocationDto> SetTouristLocation([FromBody] LocationDto location)
        {
            var result = _userService.SetUserLocation(User.PersonId(), location.Longitude, location.Latitude);
            return CreateResponse(result);
        }

    }
}
