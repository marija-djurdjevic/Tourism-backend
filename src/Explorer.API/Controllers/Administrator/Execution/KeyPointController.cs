using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.PublishRequestDtos;
using Explorer.Tours.API.Public.Authoring;
using Explorer.Tours.Core.UseCases.Authoring;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator.Execution
{

    [Authorize(Policy = "administratorPolicy")]
    [Route("api/administrator/keyPoint")]
    public class KeyPointController : BaseApiController
    {

        private readonly IKeyPointService _keyPointService;
        private readonly IPublishRequestService _publishRequestService;

        public KeyPointController(IKeyPointService keyPointService, IPublishRequestService publishRequestService)
        {
            _keyPointService = keyPointService;
            _publishRequestService = publishRequestService;
        }

        [HttpGet]
        public ActionResult<PagedResult<KeyPointDto>> GetById([FromQuery] int id)
        {
            var results = _keyPointService.GetById(id);
            return CreateResponse(results);
        }

        [HttpPut("{id:int}")]
        public ActionResult<PublishRequestDto> ChangeKeyPointStatus([FromBody] PublishRequestDto publishRequest)
        {
            // Update the publish request
            var result = _publishRequestService.Update(publishRequest);

            if (publishRequest.Status == PublishRequestDto.RegistrationRequestStatus.Accepted)
            {
                _keyPointService.PublishKeyPoint(publishRequest.EntityId);
            }
            //ovdje dodaj za decline

           
            return CreateResponse(result);
        }
    }
}
