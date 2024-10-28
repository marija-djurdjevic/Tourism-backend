using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos.TourProblemDtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.UseCases.Administration;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Route("api/tourist/problem")]
    public class TourProblemController : BaseApiController
    {
        private readonly ITourProblemService _tourProblemService;
        private readonly ITourService _tourService;

        public TourProblemController(ITourProblemService tourProblemService, ITourService tourService)
        { 
            _tourProblemService = tourProblemService;
            _tourService = tourService;
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

            var list = _tourService.GetPaged(0, 0);
            if (list.Value.Results.Any(x => x.Id == problem.TourId))
            {
                var result = _tourProblemService.Create(problem);
                return CreateResponse(result);
            }
            return CreateResponse(Result.Fail("Tour with this id doesn't exist"));
        }
    }
}
