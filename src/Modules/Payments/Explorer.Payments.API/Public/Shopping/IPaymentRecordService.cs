using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos.ShoppingDtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Public.Shopping
{
    public interface IPaymentRecordService
    {
        Result<PagedResult<PaymentRecordDto>> GetPaged(int page, int pageSize);
        Result<PaymentRecordDto> Create(PaymentRecordDto paymentRecord);
        Result<PaymentRecordDto> Purchase(BundleDto bundle, int touristId);
    }
}
