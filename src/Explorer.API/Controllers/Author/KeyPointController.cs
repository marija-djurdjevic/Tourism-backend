using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.PublishRequestDtos;
using Explorer.Tours.API.Public.Authoring;
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

        public KeyPointController(IKeyPointService keyPointService, IPublishRequestService publishRequestService)
        {
            _keyPointService = keyPointService;
            _publishRequestService = publishRequestService;
        }

        [HttpGet]
        public ActionResult<PagedResult<KeyPointDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _keyPointService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<KeyPointDto> Create([FromBody] KeyPointDto keyPoint)
        {
            var result = _keyPointService.Create(keyPoint);

            //kreiranje zahtjeva sa pablisovanje kljucne tacke
            var authorId = User.PersonId();
            PublishRequestDto publishRequestDto = new PublishRequestDto();
            publishRequestDto.AuthorId = authorId;
            publishRequestDto.EntityId = result.Value.Id;
            publishRequestDto.Type = PublishRequestDto.RegistrationRequestType.KeyPoint;

            _publishRequestService.Create(publishRequestDto);



            return CreateResponse(result);
        }
    }
}
