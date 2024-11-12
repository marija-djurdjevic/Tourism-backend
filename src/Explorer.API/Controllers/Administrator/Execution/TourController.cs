using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos.TourLifecycleDtos;
using Explorer.Tours.API.Public.Authoring;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator.Execution
{
    [Authorize(Policy = "administratorPolicy")]
    [Route("api/administrator/tour")]
    public class TourController : BaseApiController
    {
        private readonly ITourService _tourService;

        public TourController(ITourService tourService)
        {
            _tourService = tourService;
        }

        [HttpGet("all")]
        public ActionResult<PagedResult<TourDto>> GetAllTours()
        {
            var result = _tourService.GetAllToursWithKeyPoints();
            return CreateResponse(result);
        }

        [HttpGet("allTours")]
        public ActionResult<PagedResult<TourDto>> GetAllAdmin()
        {
            var result = _tourService.GetPaged(0, 0);
            return CreateResponse(result);
        }

        [HttpGet("getTour")]
        public ActionResult<TourDto> GetTour([FromQuery] int tourId)
        {
            var result = _tourService.GetById(tourId);
            return CreateResponse(result);
        }

        [HttpPost("close-tour")]
        public ActionResult<TourDto> Close([FromBody] TourDto tourDto)
        {
            var result = _tourService.Close(tourDto);
            return CreateResponse(result);
        }
    }
}
