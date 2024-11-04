using Explorer.Stakeholders.API.Dtos;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.TourProblemDtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public.Execution;
using FluentResults;
using Explorer.BuildingBlocks.Core.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator.Execution
{
    [Authorize(Policy = "administratorPolicy")]
    [Route("api/administrator/problem")]
    public class TourProblemController : BaseApiController
    {
        private readonly ITourProblemService _tourProblemService;
        private readonly ITourService _tourService;

        public TourProblemController(ITourProblemService tourProblemService, ITourService tourService)
        {
            _tourProblemService = tourProblemService;
            _tourService = tourService;
        }


        [HttpPut("{id:int}")]
        public ActionResult<TourPreferencesDto> Update([FromBody] TourProblemDto tourProblem)
        {
            if (tourProblem.Deadline < DateTime.UtcNow)
            {
                var result = _tourProblemService.CloseProblem(tourProblem);
                return CreateResponse(result);
            }
            return CreateResponse(Result.Fail("deadline hasn't passed."));
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

        [HttpGet("byId")]
        public ActionResult<PagedResult<TourProblemDto>> GetById([FromQuery] int id)
        {
            var result = _tourProblemService.GetById(id);
            return CreateResponse(result);
        }

        [HttpPost("addComment")]
        public ActionResult<PagedResult<TourProblemDto>> AddComment([FromQuery] int tourProblemId, [FromBody] ProblemCommentDto commentDto)
        {
            var result = _tourProblemService.AddComment(tourProblemId, commentDto);
            return CreateResponse(result);
        }

    }
}
