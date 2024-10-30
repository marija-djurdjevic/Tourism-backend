using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Authoring;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author.Authoring

{

    [Authorize(Policy = "authorPolicy")]
    [Route("api/author/keyPoint")]
    public class KeyPointController : BaseApiController
    {
        private readonly IKeyPointService _keyPointService;

        public KeyPointController(IKeyPointService keyPointService)
        {
            _keyPointService = keyPointService;
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
            return CreateResponse(result);
        }
    }
}
