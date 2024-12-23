using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos.SecretsDtos;
using Explorer.Encounters.API.Public;
using Explorer.Tours.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Tours.API.Dtos.TourProblemDtos;

namespace Explorer.API.Controllers.Administrator
{
    [Authorize(Policy = "administratorPolicy")]
    [Route("api/administrator/stories")]
    public class StoryController : BaseApiController
    {

        private readonly IStoryService _storyService;
        public StoryController(IStoryService storyService)
        {
            _storyService = storyService;

        }

        [HttpGet("byId")]
        public ActionResult<PagedResult<StoryDto>> GetById([FromQuery] int id)
        {
            var result = _storyService.GetById(id);
            return CreateResponse(result);
        }

   

    }
}
