using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos.SecretsDtos;
using Explorer.Encounters.API.Public;
using Explorer.Tours.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Explorer.Stakeholders.Infrastructure.Authentication;
namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/storiesUnlocked")]
    public class StoryUnlockedController : BaseApiController
    {

        private readonly IStoryUnlockedService _storyUnlockedService;
        public StoryUnlockedController(IStoryUnlockedService storyUnlockedService) 
        {
            _storyUnlockedService = storyUnlockedService;
        }

        [HttpGet("all")]
        public ActionResult<List<BookDto>> GetBooksForUser()
        {
            int userId = User.PersonId();
            var results = _storyUnlockedService.GetUserBooks(userId);
            return CreateResponse(results);
        }
    }
}
