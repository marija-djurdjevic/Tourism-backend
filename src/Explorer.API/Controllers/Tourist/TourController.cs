using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Public.Shopping;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.TourLifecycleDtos;
using Explorer.Tours.API.Public.Authoring;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{

    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/tour")]
    public class TourController : BaseApiController
    {

        private readonly IShoppingService _shoppingService;
        private readonly ITourService _tourService;
        private readonly IKeyPointService _keyPointService;

        public TourController(IShoppingService shoppingService,ITourService tourService,IKeyPointService keyPointService )
        {
            _shoppingService = shoppingService;
            _tourService = tourService;
            _keyPointService = keyPointService;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult<PagedResult<TourDto>> GetAllPublished([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourService.GetAllPublished(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost("search")]
        public ActionResult<List<TourDto>> SearchTours([FromBody] SearchByDistanceDto searchByDistanceDto)
        {
            var result = _tourService.SearchTours(searchByDistanceDto);
            return CreateResponse(result);
        }

        [AllowAnonymous]
        [HttpGet("keyPoints")]
        public ActionResult<PagedResult<KeyPointDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _keyPointService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet("all")]
        public ActionResult<PagedResult<TourDto>> GetAllTours()
        {
            var result = _tourService.GetPaged(0, 0);
            return CreateResponse(result);
        }

        [HttpGet("allTours")]
        public ActionResult<List<TourDto>> GetTours()
        {
            var result = _tourService.GetAllToursWithKeyPoints();
            return CreateResponse(result);
        }

        [HttpGet("by-author")]
        public ActionResult<PagedResult<TourDto>> GetByAuthorId([FromQuery] int page, [FromQuery] int pageSize, [FromQuery] int id)
        {
            var result = _tourService.GetByAuthorId(page, pageSize, id);
            return CreateResponse(result);
        }
    }
}
