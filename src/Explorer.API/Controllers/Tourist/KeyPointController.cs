using Explorer.Tours.API.Public.Authoring;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.PublishRequestDtos;
using Explorer.Tours.API.Public.Execution;
using Explorer.Tours.Core.UseCases.Authoring;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Explorer.BuildingBlocks.Core.UseCases;

namespace Explorer.API.Controllers.Tourist
{
    
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/keyPoint")]
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

        [HttpGet("public")]
        public ActionResult<PagedResult<KeyPointDto>> GetPublic()
        {
            var results = _keyPointService.GetPublic();
            return CreateResponse(results);
        }
    }
}
