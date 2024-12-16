using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.GroupTourDtos;
using Explorer.Tours.API.Public.Authoring;
using Explorer.Tours.Core.UseCases.Authoring;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.Execution
{
    [Route("api/tourist/groupTourExecution")]
    [Authorize(Policy = "touristPolicy")]
    public class GroupTourExecutionController : BaseApiController
    {
        private readonly IGroupTourExecutionService _groupTourExecutionService;

        public GroupTourExecutionController(IGroupTourExecutionService groupTourExecutionService)
        {
            _groupTourExecutionService = groupTourExecutionService;
        }
        [HttpPost]
        public ActionResult<GroupTourExecutionDto> Create([FromBody] GroupTourExecutionDto groupTourExecution)
        {
            var result = _groupTourExecutionService.Create(groupTourExecution);
            return CreateResponse(result);
        }
    }
}

