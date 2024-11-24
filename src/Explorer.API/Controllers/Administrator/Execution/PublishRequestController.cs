using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos.PublishRequestDtos;
using Explorer.Tours.API.Public.Authoring;
using Explorer.Tours.API.Public.Execution;
using Explorer.Tours.Core.UseCases.Authoring;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator.Execution
{
    [Route("api/administrator/publishRequest")]
    [Authorize(Policy = "administratorPolicy")]
    public class PublishRequestController : BaseApiController
    {
        private readonly INotificationService _notificationService;
        private readonly ITourService _tourService;
        private readonly IPublishRequestService _publishService;
        private readonly IKeyPointService _keyPointService;

        public PublishRequestController(IPublishRequestService publishService)
        {
            _publishService = publishService;
        
        }


        [HttpPost]
        public ActionResult<PagedResult<PublishRequestDto>> Create([FromBody] PublishRequestDto publishRequest)
        {
            var result = _publishService.Create(publishRequest);
            return CreateResponse(result);
        }


        [HttpGet("getAll")]
        public ActionResult<PagedResult<PublishRequestDto>> GetAll()
        {
            var result = _publishService.GetPaged(0, 0);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<PublishRequestDto> Update([FromBody] PublishRequestDto publishRequest)
        {
            // Update the publish request
            var result = _publishService.Update(publishRequest);

            // If the publish request is accepted, update the KeyPoint status
            //if (publishRequest.Status == PublishRequestDto.RegistrationRequestStatus.Accepted)
            //{
            //    // Retrieve the KeyPoint DTO by ID
            //    var keyPointDto = _keyPointService.GetById(publishRequest.EntityId);

            //    if (keyPointDto?.Value != null) // Ensure the KeyPoint exists
            //    {
            //        // Update the KeyPoint status
            //        keyPointDto.Value.Status = Tours.API.Dtos.KeyPointDto.KeyPointStatus.Published;

            //        // Avoid tracking issues by fetching the entity from the DbContext
            //        var existingKeyPoint = _keyPointService.GetById(publishRequest.EntityId)?.Value;

            //        if (existingKeyPoint != null)
            //        {
            //            existingKeyPoint.Status = Tours.API.Dtos.KeyPointDto.KeyPointStatus.Published;
            //            _keyPointService.Update(existingKeyPoint);
            //        }
            //    }
            //    else
            //    {
            //        return NotFound($"KeyPoint with ID {publishRequest.EntityId} not found.");
            //    }
            //}

            // Return the result of the update
            return CreateResponse(result);
        }

    }
}
