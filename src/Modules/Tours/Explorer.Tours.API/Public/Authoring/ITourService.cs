using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.TourLifeCycleDtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.Authoring
{
    public interface ITourService
    {
        Result<PagedResult<TourDto>> GetPaged(int page, int pageSize);
        Result<TourDto> Create(TourDto tour);
        Result<List<TourDto>> GetByAuthorId(int page, int pageSize, int id);
        public Result<List<KeyPointDto>> GetKeyPointsByTourId(int tourId);
        public Result<List<TourDto>> GetAllToursWithKeyPoints();
        public Result<List<TourDto>> GetAllPublished(int page, int pageSize);
    }
}
