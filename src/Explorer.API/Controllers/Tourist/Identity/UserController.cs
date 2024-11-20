using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.Users;
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

        [HttpGet("getUsers")]
        public ActionResult<UserDto> GetAllUsers()
        {
            var result = _userService.GetAll();
            return CreateResponse(result);
        }

        [HttpGet("getTourists")]
        public ActionResult<UserDto> GetAllTourists()
        {
            var result = _userService.GetAllTourists();
            return CreateResponse(result);
        }

        [HttpPost("setLocation")]
        public ActionResult<LocationDto> SetTouristLocation([FromBody] LocationDto location)
        {
            var result = _userService.SetUserLocation(User.PersonId(), location.Longitude, location.Latitude);
            return CreateResponse(result);
        }

        [HttpGet("getUsername")]
        public ActionResult<string> getUsername([FromQuery] int userId)
        {
            var result = _userService.GetUsernameById(userId);
            return CreateResponse(result);
        }
    }
}
