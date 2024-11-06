using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Tours.API.Dtos.TourLifeCycleDtos;

namespace Explorer.Tours.API.Public.Administration
{
    public interface ITourReviewService
    {
        Result<PagedResult<TourReviewDto>> GetPaged(int page, int pageSize);
        Result<TourReviewDto> Create(TourReviewDto tourReview);
        Result<TourReviewDto> Get(int tourId,int userId);
        Result<TourReviewDto> Update(TourReviewDto equipment);
        Result Delete(int id);
        public bool IsTourReviewedByTourist(int userId, int tourId);
    }
}
