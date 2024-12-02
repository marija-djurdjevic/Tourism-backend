using Explorer.Payments.API.Dtos.WalletDtos;
using Explorer.Payments.API.Public.Wallet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/wallet")]
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
    }
}
