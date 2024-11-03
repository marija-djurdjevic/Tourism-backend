using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.TourLifecycleDtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public.Shopping;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{

    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/tour")]
    public class TourController : BaseApiController
    {
        private readonly IShoppingService _shoppingService;

        public TourController(IShoppingService shoppingService)
        {
            _shoppingService = shoppingService;
        }

        [HttpGet]
        public ActionResult<PagedResult<TourDto>> GetAllPublished([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _shoppingService.GetAllPublished(page, pageSize);
            return CreateResponse(result);
        }

    }
}
