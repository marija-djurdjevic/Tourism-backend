using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos.EncounterDtos;
using Explorer.Encounters.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/encounter")]
    public class EncounterController : BaseApiController
    {
        private readonly IEncounterService _encounterService;

        public EncounterController(IEncounterService encounterService)
        {
            _encounterService = encounterService;
        }

        [HttpGet]
        public ActionResult<PagedResult<EncounterDto>> GetAll()
        {
            var result = _encounterService.GetPaged(0, 0);
            return CreateResponse(result);
        }
    }
}
