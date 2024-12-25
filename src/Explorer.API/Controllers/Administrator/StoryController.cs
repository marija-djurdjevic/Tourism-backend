using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos.SecretsDtos;
using Explorer.Encounters.API.Public;
using Explorer.Tours.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Tours.API.Dtos.TourProblemDtos;
using Explorer.Tours.API.Dtos.PublishRequestDtos;
using Explorer.Tours.API.Public.Authoring;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;

namespace Explorer.API.Controllers.Administrator
{
    [Authorize(Policy = "administratorPolicy")]
    [Route("api/administrator/stories")]
    public class StoryController : BaseApiController
    {
        private readonly IPublishRequestService _publishRequestService;
        private readonly IStoryService _storyService;
        private readonly IStoryRepository _storyRepository;
        public StoryController(IStoryService storyService, IPublishRequestService publishRequestService, IStoryRepository storyRepository)
        {
            _storyService = storyService;
            _publishRequestService = publishRequestService;
            _storyRepository = storyRepository;

        }

        [HttpGet("byId")]
        public ActionResult<PagedResult<StoryDto>> GetById([FromQuery] int id)
        {
            var result = _storyService.GetById(id);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult UpdateStory([FromBody] StoryDto storyDto)
        {
            
            var result = _storyService.Update(storyDto);

            return CreateResponse(result);
        }


        [HttpPut("status/{id:int}")]
        public ActionResult<PublishRequestDto> ChangeStoryStatus([FromBody] PublishRequestDto publishRequest)
        {
            var result = _publishRequestService.Update(publishRequest);

            if (publishRequest.Status == PublishRequestDto.RegistrationRequestStatus.Rejected)
            {
                _storyRepository.Update(_storyRepository.GetById(publishRequest.EntityId));
            }

            return CreateResponse(result);
        }


    }
}
