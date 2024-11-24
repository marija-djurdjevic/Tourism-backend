using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos.WalletDtos;
using Explorer.Payments.API.Public.Wallet;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator
{
    [Authorize(Policy = "administratorPolicy")]
    [Route("api/administrator/wallet")]
    public class WalletController : BaseApiController
    {
        private readonly IWalletService _walletService;

        public WalletController(IWalletService walletService)
        {
            _walletService = walletService;
        }

        [HttpGet("balance")]
        public ActionResult<WalletDto> GetBalance()
        {
            var touristIdClaim = User.FindFirst("id")?.Value;

            if (string.IsNullOrEmpty(touristIdClaim))
            {
                return Unauthorized();
            }

            int touristId = int.Parse(touristIdClaim);
            var result = _walletService.GetByTouristId(touristId);

            return CreateResponse(result);
        }


        [HttpPut]
        public ActionResult<WalletDto> UpdateWallet([FromBody] WalletDto walletDto)
        {
            if (walletDto == null || walletDto.TouristId <= 0)
            {
                return CreateResponse(Result.Fail("Invalid wallet update request."));
            }

            var result = _walletService.Update(walletDto);

            return CreateResponse(result);
        }

        [HttpGet]
        public ActionResult<PagedResult<WalletDto>> GetAllWallets()
        {
            // Using GetPaged to fetch all wallets
            var wallets = _walletService.GetPaged(1, int.MaxValue); // Page 1 with a very large page size to fetch all

            return CreateResponse(wallets);
        }

    }
}
