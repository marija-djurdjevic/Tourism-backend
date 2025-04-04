﻿using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos.ShoppingDtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Internal.Shopping
{
    public interface ITourPurchaseTokenServiceInternal
    {
        Result<PagedResult<TourPurchaseTokenDto>> GetPaged(int page, int pageSize);
        Result<TourPurchaseTokenDto> Create(TourPurchaseTokenDto blog);
        Result<TourPurchaseTokenDto> Update(TourPurchaseTokenDto blog);
        Result Delete(int id);
        Result<List<int>> GetPurchasedTours(int touristId);

        public Result<int> RefundPurchasedTour(int tourId, int touristId);
        public TourPurchaseTokenDto FindByTourAndTourist(int tourId, int touristId);

        public Result<int> getTourByPurchaseId(int purchaseId);
    }
}
