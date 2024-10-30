using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.API.Dtos.TourLifeCycleDtos;
using Explorer.Tours.API.Public.Authoring;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.Authoring
{
    public class TourService : CrudService<TourDto, Tour>, ITourService
    {

        //private readonly IKeyPointService _keyPointService;
        //private ICrudRepository<Tour> _tourRepository;
        //private readonly ITourRepository _tourRepository;
        public TourService(ICrudRepository<Tour> repository, IMapper mapper) : base(repository, mapper) { }

        //public Result<TourDto> SearchTours(Coordinates coordinates, double distance)
        //{
        //    HashSet<int> tourIds = new HashSet<int>();
        //    var keyPoints = _keyPointService.GetAll();
        //    foreach (var kp in keyPoints)
        //    {
        //        if (true) //kp.IsInDesiredDistance(coordinates,distance)
        //            tourIds.Add(kp.TourId);
        //    }
        //    Result<TourDto> result = new Result<TourDto>();
        //    foreach (var tourId in tourIds)
        //    {
        //        var tour = _tourRepository.Get(tourId);
        //        result.WithValue(tour);
        //    }
        //    return result;
        //}
        public Result<List<TourDto>> GetByAuthorId(int id)
        {
            throw new NotImplementedException();
        }
    }
}
