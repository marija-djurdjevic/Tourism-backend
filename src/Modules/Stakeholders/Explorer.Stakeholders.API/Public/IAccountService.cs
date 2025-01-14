using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Public
{
    public interface IAccountService
    {
        Result<PagedResult<AccountReviewDto>> GetPaged(int page, int pageSize);
        Result<AccountReviewDto> BlockAccount(AccountReviewDto accountReview);
        public Result<AccountReviewDto> GetAccount(int id);
    }
}
