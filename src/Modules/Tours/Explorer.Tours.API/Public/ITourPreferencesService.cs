using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public
{
    public interface ITourPreferencesService
    {
        Result<PagedResult<TourPreferencesDto>> GetPaged(int page, int pageSize);
        Result<TourPreferencesDto> Create(TourPreferencesDto equipment);
        Result<TourPreferencesDto> Update(TourPreferencesDto equipment);
        Result Delete(int id);
        Result<TourPreferencesDto> GetByTouristId(int id);
    }
}
