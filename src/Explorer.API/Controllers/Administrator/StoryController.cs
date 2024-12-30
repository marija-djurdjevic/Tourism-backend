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
using Explorer.Encounters.Core.Domain.Secrets;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;

namespace Explorer.API.Controllers.Administrator
{
    [Authorize(Policy = "administratorPolicy")]
    [Route("api/administrator/stories")]
    public class StoryController : BaseApiController
    {
        private readonly IPublishRequestService _publishRequestService;
        private readonly IStoryService _storyService;
        private readonly IStoryRepository _storyRepository;
        private readonly IKeyPointRepository _keyPointRepository;
        private readonly IKeyPointService _keyPointService;
        public StoryController(IStoryService storyService, IPublishRequestService publishRequestService, IStoryRepository storyRepository, IKeyPointService keyPointService, IKeyPointRepository keyPointRepository)
        {
            _storyService = storyService;
            _publishRequestService = publishRequestService;
            _storyRepository = storyRepository;
            _keyPointService = keyPointService;
            _keyPointRepository = keyPointRepository;
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
                Story story = _storyRepository.GetById(publishRequest.EntityId);
                KeyPoint keyPoint = _keyPointRepository.GetByStoryId((int)story.Id);
                keyPoint.UpdateStory(
                    null
                );
                _keyPointRepository.Update(_keyPointRepository.GetByStoryId((int)story.Id));
                story.Decline();             
                _storyRepository.Update(_storyRepository.GetById(publishRequest.EntityId));
            }
            else
            {
                Story story = _storyRepository.GetById(publishRequest.EntityId);
                KeyPoint keyPoint = _keyPointRepository.GetByStoryId((int)story.Id);
                keyPoint.UpdateStory(
                    (int)story.Id
                );
                _keyPointRepository.Update(_keyPointRepository.GetByStoryId((int)story.Id));
                story.Accept();
                _storyRepository.Update(_storyRepository.GetById(publishRequest.EntityId));
            }

            return CreateResponse(result);
        }


    }
}
