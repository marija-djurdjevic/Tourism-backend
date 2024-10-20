using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator.Administration
{
    [Authorize(Policy = "administratorPolicy")]
    [Route("api/administration/account")]
    public class AccountController : BaseApiController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public ActionResult<PagedResult<AccountReviewDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _accountService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPut("block-account")]
        public ActionResult<AccountReviewDto> BlockAccount([FromBody] AccountReviewDto accountReview)
        {
            var result = _accountService.BlockAccount(accountReview);
            return CreateResponse(result);
        }
    }
}
