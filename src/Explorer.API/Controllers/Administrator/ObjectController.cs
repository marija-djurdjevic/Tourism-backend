using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.PublishRequestDtos;
using Explorer.Tours.API.Dtos.TourProblemDtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public.Authoring;
using Explorer.Tours.API.Public.Execution;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.PublishRequests;
using Explorer.Tours.Core.UseCases.Authoring;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator
{
    [Authorize(Policy = "administratorPolicy")]
    [Route("api/administrator/object")]
    public class ObjectController : BaseApiController
    {
        private readonly IObjectService _objectService;
        private readonly IPublishRequestService _publishRequestService;
        private readonly INotificationService _notificationService;

        public ObjectController(IObjectService objectService, IPublishRequestService publishRequestService, INotificationService notificationService)
        {
            _objectService = objectService;
            _publishRequestService = publishRequestService;
            _notificationService = notificationService;
        }

        [HttpGet("/by")]
        public ActionResult<PagedResult<KeyPointDto>> GetById([FromQuery] int id)
        {
            var results = _objectService.GetById(id);
            return CreateResponse(results);
        }
        [HttpGet]
        public ActionResult<PagedResult<ObjectDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _objectService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<PublishRequestDto> ChangeObjectStatus([FromBody] PublishRequestDto publishRequest)
        {
            // Update the publish request
            var result = _publishRequestService.Update(publishRequest);

            if (publishRequest.Status == PublishRequestDto.RegistrationRequestStatus.Accepted)
            {
                _objectService.PublishObject(publishRequest.EntityId, 0);
            }
            //ovdje dodaj za decline
            if (publishRequest.Status == PublishRequestDto.RegistrationRequestStatus.Rejected)
            {
                _objectService.PublishObject(publishRequest.EntityId, 1);
                notifyRejected(result.Value);
            }

            return CreateResponse(result);
        }

        private void notifyRejected(PublishRequestDto req)
        {
            int tourAuthorId = req.AuthorId;
            string content = $"Your public facility request has been rejected";
            _notificationService.Create(new NotificationDto(content, NotificationType.PublicRequest, req.Id, tourAuthorId, false));
        }
    }
}
