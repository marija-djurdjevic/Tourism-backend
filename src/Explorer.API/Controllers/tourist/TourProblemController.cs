using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.UseCases.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Route("api/tourist/problem")]
    public class TourProblemController : BaseApiController
    {
        private readonly ITourProblemService _tourProblemService;

        public TourProblemController(ITourProblemService tourProblemService)
        { 
            _tourProblemService = tourProblemService;
        }

        [HttpGet("all")]
        [Authorize(Policy = "administratorPolicy")]
        public ActionResult<PagedResult<TourProblemDto>> GetAll()
        {
            var result = _tourProblemService.GetPaged(0, 0);
            return CreateResponse(result);
        }

        [HttpPost("report")]
        [Authorize(Policy = "touristPolicy")]
        public ActionResult<TourProblemDto> Create([FromBody] TourProblemDto problem)
        {
            var result = _tourProblemService.Create(problem);
            return CreateResponse(result);
        }
    }
}
