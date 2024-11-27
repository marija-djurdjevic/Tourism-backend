using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos.WalletDtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Internal.Wallet
{
    public interface IWalletInternalService
    {
        Result<PagedResult<WalletDto>> GetPaged(int page, int pageSize);
        Result<WalletDto> Create(WalletDto blog);
        Result<WalletDto> Update(WalletDto blog);
        Result Delete(int id);
        Result<WalletDto> GetByTouristId(int touristId);
    }
}
