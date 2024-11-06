﻿using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.TourLifecycleDtos;
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
        private readonly IKeyPointService _keyPointService;

        public TourController(IShoppingService shoppingService,ITourService tourService,IKeyPointService keyPointService )
        {
            _shoppingService = shoppingService;
            _tourService = tourService;
            _keyPointService = keyPointService;
        }

        [HttpGet]
        public ActionResult<PagedResult<TourDto>> GetAllPublished([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourService.GetAllPublished(page, pageSize);
            return CreateResponse(result);
        }

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
    }
}
