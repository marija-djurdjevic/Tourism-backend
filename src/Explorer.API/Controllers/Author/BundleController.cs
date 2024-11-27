using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Public.Shopping;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Explorer.Payments.API.Dtos.ShoppingDtos;
using Explorer.Tours.API.Dtos.TourLifecycleDtos;
using Explorer.Tours.API.Public.Authoring;

namespace Explorer.API.Controllers.Author
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/author/bundle")]
    public class BundleController : BaseApiController
    {
        private readonly IBundleService bundleService;
        private readonly ITourService tourService;

        public BundleController(IBundleService bundleService, ITourService tourService)
        {
            this.bundleService = bundleService;
            this.tourService = tourService;
        }

        [HttpGet]
        public ActionResult<PagedResult<BundleDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = bundleService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet("{authorId}")]
        public ActionResult<List<BundleDto>> GetByAuthorId(int authorId)
        {
            var result = bundleService.GetByAuthorId(authorId);
            return CreateResponse(result);
        }

        [HttpGet("{authorId}/{bundleId}")]
        public ActionResult<List<TourDto>> GetToursByBundleId(int authorId, int bundleId)
        {
            var pagedResult = tourService.GetByAuthorId(1, 100, authorId);
            var allTours = pagedResult.Value.Results;
            
            var bundle = bundleService.GetById(bundleId);

            var filteredTours = allTours.Where(tour => bundle.Value.TourIds.Contains(tour.Id)).ToList();

            return Ok(filteredTours);
        }

        [HttpPost]
        public ActionResult<BundleDto> Create([FromBody] BundleDto bundle)
        {
            var result = bundleService.Create(bundle);
            return CreateResponse(result);
        }

        [HttpPut]
        public ActionResult<BundleDto> Update([FromBody] BundleDto bundle)
        {
            var result = bundleService.Update(bundle);
            return CreateResponse(result);
        }

    }
}
