using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.PublishRequestDtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public.Authoring;
using Explorer.Tours.Core.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/author/object")]
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

            var filteredResults = result.Value.Results.Where(o => o.Status != ObjectDto.ObjectStatus.Pending && o.Status != ObjectDto.ObjectStatus.Rejected)
                .ToList();

           
            var filteredResult = new PagedResult<ObjectDto>(
                filteredResults,
                result.Value.TotalCount - result.Value.Results.Count(o => o.Status == ObjectDto.ObjectStatus.Pending)
            );

            var response = FluentResults.Result.Ok(filteredResult);

            return CreateResponse(response);
        }

        [HttpPost]
        public ActionResult<ObjectDto> Create([FromBody] ObjectDto touristObject)
        {
            var result = _objectService.Create(touristObject);

            if (touristObject.Status == ObjectDto.ObjectStatus.Pending)
            {
                var authorId = User.PersonId();
                if (result.IsSuccess)
                {
                    PublishRequestDto publishRequestDto = new PublishRequestDto();
                    publishRequestDto.AuthorId = authorId;
                    publishRequestDto.EntityId = result.Value.Id;
                    publishRequestDto.Type = PublishRequestDto.RegistrationRequestType.Object;

                    _publishRequestService.Create(publishRequestDto);
                }
            }


            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<ObjectDto> Update([FromBody] ObjectDto touristObject)
        {
            var result = _objectService.Update( touristObject);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _objectService.Delete(id);
            return CreateResponse(result);
        }
    }
}
