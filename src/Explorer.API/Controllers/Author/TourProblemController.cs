using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos.TourProblemDtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public.Execution;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author
{
    [Route("api/author/problem")]
    [Authorize(Policy = "authorPolicy")]
    public class TourProblemController : BaseApiController
    {
        private readonly ITourProblemService _tourProblemService;
        private readonly ITourService _tourService;

        public TourProblemController(ITourProblemService tourProblemService, ITourService tourService)
        {
            _tourProblemService = tourProblemService;
            _tourService = tourService;
        }

        [HttpGet("getByAuthorId")]
        public ActionResult<PagedResult<TourProblemDto>> GetByAuthorId([FromQuery] int authorId)
        {
            var tours = _tourService.GetByAuthorId(authorId);
            var tourIds = tours.Value.Select(tour => tour.Id).ToList();
            var results = _tourProblemService.GetByToursIds(tourIds);

            return CreateResponse(results);
        }
    }
}
