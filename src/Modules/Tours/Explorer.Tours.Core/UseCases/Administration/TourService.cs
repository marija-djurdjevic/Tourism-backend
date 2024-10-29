using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.Administration
{
    public class TourService : CrudService<TourDto, Tour>, ITourService
    {
        public TourService(ICrudRepository<Tour> repository, IMapper mapper) : base(repository, mapper) { }

        public Result<List<TourDto>> GetByAuthorId(int id)
        {
            int i = 1;
            var list = GetPaged(i, 20);

            do
            {
                if (list.Value.Results.Any(x => x.AuthorId == id))
                {
                    return Result.Ok(list.Value.Results.Where(x => x.AuthorId == id).ToList());
                }

                i++;
                list = GetPaged(i, 20);
            } while (list.Value.Results.Count > 0);

            return Result.Fail("This user doesn't have preference settings");
        }

        public Result<TourDto> GetById(int id)
        {
            var list = GetPaged(0, 0);
            var tour = list.Value.Results.First(x => x.Id == id);
            if (tour == null)
                return Result.Fail("Tour  not found");

            return Result.Ok(tour);
        }
    }
}
