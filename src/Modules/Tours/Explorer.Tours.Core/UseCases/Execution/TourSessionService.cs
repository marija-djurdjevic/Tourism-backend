using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.TourSessionDtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public.Execution;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.TourSessions;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.Execution
{
    public class TourSessionService : BaseService<TourSessionDto, TourSession>, ITourSessionService
    {
        private readonly ITourSessionRepository _repository;
        private readonly IMapper _mapper;

        public TourSessionService(IMapper mapper, ITourSessionRepository repository) : base(mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Result<TourSessionDto> AbandonTour(int tourSessionId)
        {
            var tourSession = _repository.Get(tourSessionId);

            if (tourSession == null)
            {
                return Result.Fail<TourSessionDto>("Tour session not found.");
            }

            tourSession.AbandonSession();
            _repository.Update(tourSession);

            return Result.Ok(_mapper.Map<TourSession, TourSessionDto>(tourSession));
        }

        

        public Result<TourSessionDto> CompleteTour(int tourSessionId)
        {
            var tourSession = _repository.Get(tourSessionId);

            if (tourSession == null)
            {
                return Result.Fail<TourSessionDto>("Tour session not found.");
            }

            tourSession.CompleteSession();
            _repository.Update(tourSession);

            return Result.Ok(_mapper.Map<TourSession, TourSessionDto>(tourSession));
        }

        public Result<TourSessionDto> StartTour(int tourId, LocationDto initialLocation)
        {

            var location = _mapper.Map<LocationDto, Location>(initialLocation);

           
            var tourSession = new TourSession(tourId, location);

            if (tourSession == null)
            {
                return Result.Fail<TourSessionDto>("Tour session not found.");
            }

            _repository.Create(tourSession);

            return Result.Ok(_mapper.Map<TourSession, TourSessionDto>(tourSession));
        }
    }


}
