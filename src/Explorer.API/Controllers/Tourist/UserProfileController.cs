using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/profile")]
    public class UserProfileController : BaseApiController
    {
        private readonly IUserProfileService _userProfileService;
        private readonly IUserService _userService;
        private readonly IAccountService _accountService;

        public UserProfileController(IUserProfileService userProfileService, IUserService userService, IAccountService accountService)
        {
            _userProfileService = userProfileService;
            _userService = userService;
            _accountService = accountService;
        }

        [HttpGet]
        public ActionResult<UserProfileDto> Get()
        {
            var userId = User.FindFirst("id")?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var result = _userProfileService.Get(Int32.Parse(userId));
            result.Value.Email = _accountService.GetAccount(Int32.Parse(userId)).Value.Email;
            result.Value.XP = _userService.GetUserById(Int32.Parse(userId)).Value.XP;
            return CreateResponse(result);
        }

        [HttpGet("{userId:long}")]
        public ActionResult<UserProfileDto> GetById(int userId)
        {
            var result = _userProfileService.Get(userId);
            return CreateResponse(result);
        }

        [HttpPut]
        public ActionResult<UserProfileDto> Update([FromBody] UserProfileDto profile)
        {
            var result = _userProfileService.Update(profile);
            return CreateResponse(result);
        }

    }
}
