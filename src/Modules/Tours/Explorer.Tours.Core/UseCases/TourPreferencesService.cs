using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases
{
    public class TourPreferencesService : CrudService<TourPreferencesDto, TourPreferences>, ITourPreferencesService
    {
        public TourPreferencesService(ICrudRepository<TourPreferences> repository, IMapper mapper) : base(repository, mapper) { }

        public Result<TourPreferencesDto> GetByTouristId(int id)
        {
            int i = 1;
            var list = GetPaged(i, 20);

            do
            {
                if(list.Value.Results.Any(x => x.TouristId == id))
                {
                    return Result.Ok(list.Value.Results.First(x => x.TouristId == id));
                }

                i++;
                list = GetPaged(i, 20);
            } while (list.Value.Results.Count > 0);

            return Result.Fail("This user doesn't have preference settings");
        }
    }
}
