using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos.TourProblemDtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public.Execution;
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
            throw new NotImplementedException("This method has not been implemented yet.");
        }

        [HttpGet("all")]
        [Authorize(Policy = "administratorPolicy")]
        public ActionResult<PagedResult<TourProblemDto>> GetAll()
        {
            throw new NotImplementedException("This method has not been implemented yet.");
        }

        [HttpPost("report")]
        [Authorize(Policy = "touristPolicy")]
        public ActionResult<TourProblemDto> Create([FromBody] TourProblemDto problem)
        {
            throw new NotImplementedException("This method has not been implemented yet.");
        }
    }
}
