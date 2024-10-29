using Explorer.Blog.API.Dtos;
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
    [Authorize(Policy = "touristPolicy")]
    public class TourProblemController : BaseApiController
    {
        private readonly ITourProblemService _tourProblemService;

        public TourProblemController(ITourProblemService tourProblemService, ITourService tourService)
        {
            _tourProblemService = tourProblemService;
        }

        [HttpGet("getByTouristId")]
        public ActionResult<PagedResult<TourProblemDto>> GetByTouristId([FromQuery] int touristId)
        {
            var result = _tourProblemService.GetByTouristId(touristId);
            return CreateResponse(result);
        }

        [HttpPost("create")] 
        public ActionResult<PagedResult<TourProblemDto>> Create(TourProblemDto tourProblemDto)
        {
            var result = _tourProblemService.Create(tourProblemDto);
            return CreateResponse(result);
        }

        [HttpPost("addComment")]
        public ActionResult<PagedResult<TourProblemDto>> AddComment([FromQuery] int tourProblemId, ProblemCommentDto commentDto)
        {
            var result = _tourProblemService.AddComment(tourProblemId, commentDto);
            return CreateResponse(result);
        }

        [HttpPut("changeStatus")]
        public ActionResult<PagedResult<TourProblemDto>> ChangeStatus([FromQuery] int tourProblemId, ProblemStatus problemStatus)
        {
            var result = _tourProblemService.ChangeStatus(tourProblemId, problemStatus);
            return CreateResponse(result);
        }
    }
}
