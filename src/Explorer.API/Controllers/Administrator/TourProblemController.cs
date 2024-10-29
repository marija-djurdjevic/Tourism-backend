using Explorer.Stakeholders.API.Dtos;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.TourProblemDtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public.Execution;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator
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
            if(tourProblem.Deadline < DateTime.UtcNow)
            {
                var result = _tourProblemService.CloseProblem(tourProblem);
                return CreateResponse(result);
            }
            return CreateResponse(Result.Fail("deadline hasn't passed."));  
        }

    }
}
