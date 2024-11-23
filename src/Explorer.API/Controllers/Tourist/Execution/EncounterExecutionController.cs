using Explorer.Encounters.API.Dtos.EncounterDtos;
using Explorer.Encounters.API.Dtos.EncounterExecutionDtos;
using Explorer.Encounters.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.Execution
{ 
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/encounterExecution")]
    public class EncounterExecutionController : BaseApiController
    {
        private readonly IEncounterExecutionService _encounterExecutionService;

        public EncounterExecutionController(IEncounterExecutionService encounterExecutionService)
        {
            _encounterExecutionService = encounterExecutionService;
        }

        [HttpPost("create")]
        public ActionResult<EncounterExecutionDto> Create([FromBody] EncounterExecutionDto encounterExecutionDto)
        {
            var result = _encounterExecutionService.Create(encounterExecutionDto);
            return CreateResponse(result);
        }
    }
}
