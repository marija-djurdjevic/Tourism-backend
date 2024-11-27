using Explorer.Blog.API.Dtos;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos.ShoppingDtos;
using Explorer.Payments.API.Public.Shopping;
using Explorer.Tours.API.Dtos;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/author/sale")]
    public class SaleController : BaseApiController
    {
        private readonly ISaleService _saleService;

        public SaleController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        [HttpGet("{saleId}")]
        public ActionResult<SaleDto> GetSaleById(int saleId)
        {
            var result = _saleService.GetSaleById(saleId);
            return CreateResponse(result);
        }

        [HttpGet]
        public ActionResult<PagedResult<SaleDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _saleService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<SaleDto> Create([FromBody] SaleDto sale)
        {
            var result = _saleService.Create(sale);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<SaleDto> Update([FromBody] SaleDto sale)
        {
            var result = _saleService.Update(sale);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _saleService.Delete(id);
            return CreateResponse(result);
        }
    }
}
