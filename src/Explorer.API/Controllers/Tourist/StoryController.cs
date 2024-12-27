using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos.SecretsDtos;
using Explorer.Encounters.API.Public;
using Explorer.Tours.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Tours.API.Dtos.TourProblemDtos;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/stories")]
    public class StoryController : BaseApiController
    {

        private readonly IStoryService _storyService;
        private readonly IStoryUnlockedService _storyUnlockedService;

        public StoryController(IStoryService storyService, IStoryUnlockedService storyUnlockedService)
        {
            _storyService = storyService;
            _storyUnlockedService = storyUnlockedService;

        }

        [HttpGet("byId")]
        public ActionResult<PagedResult<StoryDto>> GetById([FromQuery] int id)
        {
            var result = _storyService.GetById(id);
            return CreateResponse(result);
        }

        [HttpGet("byBookId")]
        public ActionResult<PagedResult<StoryDto>> GetByBookId([FromQuery] int id)
        {
            var result = _storyService.GetByBookId(id);
            return CreateResponse(result);
        }

        [HttpGet("unlockStory")]
        public ActionResult<bool> UnlockStory([FromQuery] int storyId)
        {
            int userId = User.PersonId();

            var storyUnlocked = new StoryUnlockedDto
            {
                StoryId = storyId,
                UserId = userId
            };

            var result = _storyUnlockedService.Create(storyUnlocked);
            return Ok(true);
        }

    }
}
