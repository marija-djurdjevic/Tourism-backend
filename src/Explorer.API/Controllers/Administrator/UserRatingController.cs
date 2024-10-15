using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator
{
    [Authorize(Policy = "administratorPolicy")]
    [Route("api/administrator/ratings")]
    public class UserRatingController : BaseApiController
    {
        private readonly IUserRatingService _userRatingService;

        public UserRatingController(IUserRatingService userRatingService)
        {
            _userRatingService = userRatingService;
        }

        [HttpGet]
        public ActionResult<List<UserRatingDto>> GetAll()
        {
            var result = _userRatingService.GetAll();
            return CreateResponse(result);
        }

    }
}
