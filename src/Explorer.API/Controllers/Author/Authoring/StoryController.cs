using Explorer.Encounters.API.Dtos.SecretsDtos;
using Explorer.Encounters.API.Public;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Tours.API.Dtos;
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
        private readonly IKeyPointService _keyPointService;

        public StoryController(IStoryService storyService, IPublishRequestService publishRequestService, IKeyPointService keyPointService)
        {
            _storyService = storyService;
            _publishRequestService = publishRequestService;
            _keyPointService = keyPointService;
        }

        [HttpPost]
        public ActionResult<StoryDto> Create([FromBody] StoryDto story, [FromQuery] int keyId)
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

            KeyPointDto keyPoint = _keyPointService.GetById(keyId).Value;
            keyPoint.StoryId = result.Value.Id;
            _keyPointService.UpdateKeyPointStory(keyPoint.Id, keyPoint);
            return CreateResponse(result);
        }
    }
}
