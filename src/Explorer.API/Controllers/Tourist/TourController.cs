﻿using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.TourLifeCycleDtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public.Authoring;
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
        private readonly ITourService _tourService;

        public TourController(IShoppingService shoppingService, ITourService tourService)
        {
            _shoppingService = shoppingService;
            _tourService = tourService;
        }

        [HttpGet]
        public ActionResult<PagedResult<TourDto>> GetAllPublished([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _shoppingService.GetAllPublished(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost("search")]
        public ActionResult<List<TourDto>> SearchTours([FromBody] SearchByDistanceDto searchByDistanceDto)
        {
            var result = _tourService.SearchTours(searchByDistanceDto);
            return CreateResponse(result);
        }

    }
}
