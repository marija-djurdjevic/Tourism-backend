using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos.TourProblemDtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public.Execution;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator
{

    [Route("api/author/problem")]
    [Authorize(Policy = "administratorPolicy")]
    public class TourProblemController : BaseApiController
    {
        private readonly ITourProblemService _tourProblemService;
        private readonly ITourService _tourService;

        public TourProblemController(ITourProblemService tourProblemService, ITourService tourService)
        {
            _tourProblemService = tourProblemService;
            _tourService = tourService;
        }

        [HttpGet("getAll")]
        public ActionResult<PagedResult<TourProblemDto>> GetAll()
        {
            var results = _tourProblemService.GetAll();
            return CreateResponse(results);
        }

        [HttpPost("setDeadline")]
        public ActionResult<PagedResult<TourProblemDto>> SetDeadline([FromQuery] int problemId, [FromQuery] DateTime time)
        {
            var problem = _tourProblemService.GetById(problemId);
            var authorId = _tourService.GetById(problem.Value.TourId).Value.AuthorId;
            var result = _tourProblemService.SetDeadline(problemId, time, authorId);
            return CreateResponse(result);
        }
    }
}
