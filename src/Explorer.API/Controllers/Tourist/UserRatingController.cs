using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/ratings")]
    public class UserRatingController : BaseApiController
    {
        private readonly IUserRatingService _userRatingService;

        public UserRatingController(IUserRatingService userRatingService)
        {
            _userRatingService = userRatingService;
        }

        

        [HttpPost]
        public ActionResult<UserRatingDto> Create([FromBody] UserRatingDto rating)
        {
            var userId = User.FindFirst("id").Value;
            var username = User.FindFirst("username").Value;
            var result = _userRatingService.Create(rating, userId, username);
            return CreateResponse(result);
        }

    }
}
