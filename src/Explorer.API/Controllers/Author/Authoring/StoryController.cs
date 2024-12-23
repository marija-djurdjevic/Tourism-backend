using Explorer.Encounters.API.Dtos.SecretsDtos;
using Explorer.Encounters.API.Public;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Tours.API.Dtos.PublishRequestDtos;
using Explorer.Tours.API.Public.Authoring;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author.Authoring
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/author/story")]
    public class StoryController : BaseApiController
    {
        private readonly IStoryService _storyService;
        private readonly IPublishRequestService _publishRequestService;

        public StoryController(IStoryService storyService, IPublishRequestService publishRequestService)
        {
            _storyService = storyService;
            _publishRequestService = publishRequestService;
        }

        [HttpPost]
        public ActionResult<StoryDto> Create([FromBody] StoryDto story)
        {
            int userId = User.PersonId();
            
            story.AuthorId = userId;
            story.StoryStatus = StoryStatus.Pending;

            var result = _storyService.Create(story);
         
            if (result.IsSuccess)
            {
                PublishRequestDto publishRequestDto = new PublishRequestDto();
                publishRequestDto.AuthorId = userId;
                publishRequestDto.EntityId = result.Value.Id;
                publishRequestDto.Type = PublishRequestDto.RegistrationRequestType.Story;

                _publishRequestService.Create(publishRequestDto);
            }
            return CreateResponse(result);
        }
    }
}
