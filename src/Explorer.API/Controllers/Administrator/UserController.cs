using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator
{
    [Authorize(Policy = "administratorPolicy")]
    [Route("api/administrator/users")]
    public class UserController : BaseApiController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        // Check if user exists by username
        [HttpGet("exists/{username}")]
        public ActionResult<bool> Exists(string username)
        {
            var result = _userService.Exists(username);
            return CreateResponse(result);
        }

        // Get active user by username
        [HttpGet("{username}")]
        public ActionResult<UserDto> GetActiveByName(string username)
        {
            var result = _userService.GetActiveByName(username);
            return CreateResponse(result);
        }

        // Get person ID by user ID
        [HttpGet("{userId}/person-id")]
        public ActionResult<long> GetPersonId(long userId)
        {
            var result = _userService.GetPersonId(userId);
            return CreateResponse(result);
        }

        [HttpGet("{userId}/person")]
        public ActionResult<PersonDto> GetPersonByUserId(long userId)
        {
            var result = _userService.GetPersonByUserId(userId);
            return CreateResponse(result);
        }
    }
}
