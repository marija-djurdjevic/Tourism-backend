using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Public.Shopping;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Explorer.Payments.API.Dtos.ShoppingDtos;

namespace Explorer.API.Controllers.Author
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/author/bundle")]
    public class BundleController : BaseApiController
    {
        private readonly IBundleService bundleService;

        public BundleController(IBundleService bundleService)
        {
            this.bundleService = bundleService;
        }

        [HttpGet]
        public ActionResult<PagedResult<BundleDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = bundleService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet("{authorId}")]
        public ActionResult<BundleDto> GetByAuthorId(int authorId)
        {
            var result = bundleService.GetByAuthorId(authorId);
            return CreateResponse(result);
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
