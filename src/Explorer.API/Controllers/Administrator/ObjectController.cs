using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.PublishRequestDtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public.Authoring;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.PublishRequests;
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

        public ObjectController(IObjectService objectService, IPublishRequestService publishRequestService)
        {
            _objectService = objectService;
            _publishRequestService = publishRequestService;
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
                _objectService.PublishObject(publishRequest.EntityId);
            }
            //ovdje dodaj za decline


            return CreateResponse(result);
        }
    }
}
