using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos.PublishRequestDtos;
using Explorer.Tours.API.Public.Authoring;
using Explorer.Tours.API.Public.Execution;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Tours.Core.UseCases.Authoring;

namespace Explorer.API.Controllers.Author.Execution
{
    [Route("api/author/publishRequest")]
    [Authorize(Policy = "authorPolicy")]
    public class PublishRequestController : BaseApiController
    {

        private readonly INotificationService _notificationService;
        private readonly ITourService _tourService;
        private readonly IPublishRequestService _publishService;
        private readonly IKeyPointService _keyPointService;

        public PublishRequestController(IPublishRequestService publishService, IKeyPointService keyPointService)
        {
            _publishService = publishService;
            _keyPointService = keyPointService;
        }


        [HttpGet("getAll")]
        public ActionResult<PagedResult<PublishRequestDto>> GetAll()
        {
            var result = _publishService.GetPaged(0, 0);

            // notifyCreatedReport(tourProblemDto);
            return CreateResponse(result);
        }


        [HttpPost("create")]
        public ActionResult<PagedResult<PublishRequestDto>> Create(PublishRequestDto publishRequestDto)
        {
            var result = _publishService.Create(publishRequestDto);
           // notifyCreatedReport(tourProblemDto);
            return CreateResponse(result);
        }
    }
}
