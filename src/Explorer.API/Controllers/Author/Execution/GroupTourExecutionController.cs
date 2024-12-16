using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.GroupTourDtos;
using Explorer.Tours.API.Public.Authoring;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author.Execution
{
    [Route("api/author/groupTourExecution")]
    [Authorize(Policy = "authorPolicy")]
    public class GroupTourExecutionController : BaseApiController
    {
        private readonly IGroupTourExecutionService _groupTourExecutionService;

        public GroupTourExecutionController(IGroupTourExecutionService groupTourExecutionService)
        {
            _groupTourExecutionService = groupTourExecutionService;
        }

        [HttpGet]
        public ActionResult<PagedResult<GroupTourExecutionDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _groupTourExecutionService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

    }
}
