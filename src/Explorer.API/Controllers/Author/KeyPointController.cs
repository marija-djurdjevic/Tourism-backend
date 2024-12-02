using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.PublishRequestDtos;
using Explorer.Tours.API.Public.Authoring;
using Explorer.Tours.API.Public.Execution;
using Explorer.Tours.Core.UseCases.Authoring;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author

{

    [Authorize(Policy = "authorPolicy")]
    [Route("api/author/keyPoint")]
    public class KeyPointController : BaseApiController
    {
        private readonly IKeyPointService _keyPointService;
        private readonly IPublishRequestService _publishRequestService;
        private readonly INotificationService _notificationService;

        public KeyPointController(IKeyPointService keyPointService, IPublishRequestService publishRequestService, INotificationService notificationService)
        {
            _keyPointService = keyPointService;
            _publishRequestService = publishRequestService;
            _notificationService = notificationService;
        }
        
        [HttpGet]
        public ActionResult<PagedResult<KeyPointDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _keyPointService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<PublishRequestDto> AddTourToKeyPoint([FromBody] KeyPointDto kp, [FromQuery] long id)
        {
            List<long> longIds = kp.TourIds.Select(i => (long)i).ToList();
            longIds.Add(id);
            var result = _keyPointService.UpdateList(kp.Id, longIds);
            return CreateResponse(result);
        }


        [HttpGet("public")]
        public ActionResult<PagedResult<KeyPointDto>> GetPublic()
        {
            var results = _keyPointService.GetPublic();
            return CreateResponse(results);
        }

        [HttpPost]
        public ActionResult<KeyPointDto> Create([FromBody] KeyPointDto keyPoint)
        {
            if (string.IsNullOrWhiteSpace(keyPoint.Name))
            {
                return BadRequest("Name is required.");
            }
            var result = _keyPointService.Create(keyPoint);

            //kreiranje zahtjeva sa pablisovanje kljucne tacke
            if(keyPoint.Status == KeyPointDto.KeyPointStatus.Pending)
            {
                var authorId = User.PersonId();
                if (result.IsSuccess)
                {
                    PublishRequestDto publishRequestDto = new PublishRequestDto();
                    publishRequestDto.AuthorId = authorId;
                    publishRequestDto.EntityId = result.Value.Id;
                    publishRequestDto.Type = PublishRequestDto.RegistrationRequestType.KeyPoint;

                    _publishRequestService.Create(publishRequestDto);
                }
            }
           

            return CreateResponse(result);
        }
    }
}
