using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.Core.Domain.Users;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.TourSessionDtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public.Authoring;
using Explorer.Tours.API.Public.Execution;
using Explorer.Tours.API.Public.Shopping;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
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
        private readonly ITourService _tourService;
        private readonly IKeyPointService _keyPointService;
        private readonly ITourPurchaseTokenRepository _purchaseTokenRepository;


        public TourSessionService(IMapper mapper, ITourSessionRepository repository, ITourService tourService, IKeyPointService keyPointService, ITourPurchaseTokenRepository tourPurchaseTokenRepository) : base(mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _tourService = tourService;
            _keyPointService = keyPointService;
            _purchaseTokenRepository = tourPurchaseTokenRepository;

        }
        public Result<TourSessionDto> AbandonTour(int tourId,int userId)
        {

            var allSessions = _repository.GetPaged(1, int.MaxValue).Results;


            var tourSession = allSessions.FirstOrDefault(session =>
                session.TourId == tourId && session.UserId==userId);




            if (tourSession == null)
            {
                return Result.Fail<TourSessionDto>("Tour session not found.");
            }

            tourSession.AbandonSession();
            _repository.Update(tourSession);

            return Result.Ok(_mapper.Map<TourSession, TourSessionDto>(tourSession));
        }



        public Result<TourSessionDto> CompleteTour(int tourId,int userId)
        {
            var allSessions = _repository.GetPaged(1, int.MaxValue).Results;


            var tourSession = allSessions.FirstOrDefault(session =>
                session.TourId == tourId && session.UserId==userId);


            if (tourSession == null)
            {
                return Result.Fail<TourSessionDto>("Tour session not found.");
            }

            tourSession.CompleteSession();
            _repository.Update(tourSession);

            return Result.Ok(_mapper.Map<TourSession, TourSessionDto>(tourSession));
        }

        public Result<TourSessionDto> StartTour(int tourId, int userId, LocationDto initialLocation)
        {

            var purchasedTours = _purchaseTokenRepository.GetPurchasedTours(userId);

            bool exist = purchasedTours.Any(item => item == tourId);
            if (!exist)
            {
                return Result.Fail<TourSessionDto>("Tour not found.");

            }



            var tourResult = _tourService.Get(tourId);

            var tour = tourResult.Value;

            TourStatus tourStatus = (TourStatus)Enum.Parse(typeof(TourStatus), tour.Status.ToString());

            if (tour == null)
            {
                return Result.Fail<TourSessionDto>("Tour not found.");
            }


            if (tourStatus != TourStatus.Published && tourStatus != TourStatus.Archived)
            {
                return Result.Fail<TourSessionDto>("Tour is not in a state that allows starting a session.");
            }


            var allSessions = _repository.GetPaged(1, int.MaxValue).Results;


            var existingSession = allSessions.FirstOrDefault(session =>
                session.TourId == tourId && session.UserId == userId);

            if (existingSession != null)
            {
                return Result.Fail<TourSessionDto>("An active tour session already exists for this tour.");
            }



            var location = _mapper.Map<LocationDto, Domain.TourSessions.Location>(initialLocation);


            var tourSession = new TourSession(tourId, location, userId);

            if (tourSession == null)
            {
                return Result.Fail<TourSessionDto>("Tour session not found.");
            }

            _repository.Create(tourSession);

            return Result.Ok(_mapper.Map<TourSession, TourSessionDto>(tourSession));
        }




        public bool UpdateLocation(int tourId, LocationDto locationDto,int userId)
        {

            var location = _mapper.Map<LocationDto, Domain.TourSessions.Location>(locationDto);

            var keyPointsResult = _keyPointService.GetKeyPointsByTourId(tourId);



            var keyPoints = keyPointsResult.Value;

            if (keyPoints == null || !keyPoints.Any())
            {
                throw new Exception($"No keypoints found for tour ID {tourId}.");
            }


            var lastKeyPoint = keyPoints.Last();
            var lastKeyPointLocation = new Domain.TourSessions.Location(lastKeyPoint.Latitude, lastKeyPoint.Longitude);

            bool isNear = Domain.TourSessions.Location.IsWithinSimpleDistance(location, lastKeyPointLocation);
            var allSessions = _repository.GetPaged(1, int.MaxValue).Results;
            var existingSession = allSessions.FirstOrDefault(session =>
                session.TourId == tourId && session.UserId==userId);


            if (isNear)
            {
                existingSession.UpdateCurrentLocation(location);
                existingSession.CompleteSession();
                _repository.Update(existingSession);
            }

            existingSession.UpdateCurrentLocation(location);
            _repository.Update(existingSession);

            return isNear;
        }







        public void UpdateSession(int tourId, LocationDto locationDto,int userId)
        {

            var location = _mapper.Map<LocationDto, Domain.TourSessions.Location>(locationDto);


            var allSessions = _repository.GetPaged(1, int.MaxValue).Results;

            var existingSession = allSessions.FirstOrDefault(session =>
                session.TourId == tourId && session.UserId == userId);


            existingSession.UpdateCurrentLocation(location);
            _repository.Update(existingSession);
        }

        public bool CanUserReviewTour(int tourId, int userId)
        {
            var allSessions = _repository.GetPaged(0, 0).Results;

            var existingSession = allSessions.FirstOrDefault(session =>
                session.TourId == tourId && session.UserId == userId);

            if (existingSession == null)
            {
                return false;
            }

            int keyPointsCount = _keyPointService.GetKeyPointsByTourId(tourId).Value.Count;
            var tourProgressPercentage = (int)((double)1/*existingSession.CompletedKeyPoints.Count*/ / (keyPointsCount <= 0 ? 1 : keyPointsCount) * 100);

            if (DateTime.UtcNow < existingSession.LastActivity.AddDays(7) &&
                DateTime.UtcNow > existingSession.LastActivity && tourProgressPercentage > 35)
            {
                return true;
            }

            return false;
        }

        public (int, DateTime) GetProgressAndLastActivity(int tourId, int userId)
        {
            var allSessions = _repository.GetPaged(0, 0).Results;

            var existingSession = allSessions.FirstOrDefault(session =>
                session.TourId == tourId && session.UserId == userId);

            if (existingSession == null)
            {
                return (0, DateTime.UtcNow);
            }

            int keyPointsCount = _keyPointService.GetKeyPointsByTourId(tourId).Value.Count;
            var tourProgressPercentage = (int)((double)1 /*existingSession.CompletedKeyPoints.Count*/ / (keyPointsCount <= 0 ? 1 : keyPointsCount) * 100);

            return (tourProgressPercentage, existingSession.LastActivity);
        }

        public Result<TourSessionDto> UpdateLastActivity(int tourId, int userId)
        {
            var tourSession = _repository.GetByTourId(tourId, userId);

            if (tourSession == null)
            {
                return Result.Fail<TourSessionDto>("Tour session not found.");
            }

            tourSession.UpdateLastActivity();

            _repository.Update(tourSession);

            return Result.Ok(_mapper.Map<TourSession, TourSessionDto>(tourSession));
        }

        public Result<List<CompletedKeyPointDto>> GetCompletedKeyPoints(int tourId, int userId)
        {
            var tourSession = _repository.GetByTourId(tourId, userId);

            if (tourSession == null)
            {
                return Result.Fail<List<CompletedKeyPointDto>>("Tour session not found.");
            }

            var completedKeyPoints = _mapper.Map<List<CompletedKeyPointDto>>(tourSession.CompletedKeyPoints);

            // Return the result as a successful operation
            return Result.Ok(completedKeyPoints);
        }

        public Result<TourSessionDto> AddCompletedKeyPoint(long tourId, long keyPointId, int userId)
        {
            var tourSession = _repository.GetByTourId(tourId, userId);

            if (tourSession == null)
            {
                return Result.Fail<TourSessionDto>("Tour session not found.");
            }

            CompletedKeyPoints newKeypoint = new CompletedKeyPoints((int)keyPointId, DateTime.UtcNow);
            tourSession.CompletedKeyPoints.Add(newKeypoint);

            _repository.Update(tourSession);

            return Result.Ok(_mapper.Map<TourSession, TourSessionDto>(tourSession));
        }

        public Result<List<KeyPointDto>> GetKeyPointsByTourId(int tourId)
        {
            var keyPointsResult = _keyPointService.GetKeyPointsByTourId(tourId);



            var keyPoints = keyPointsResult.Value;

            if (keyPoints == null || !keyPoints.Any())
            {
                throw new Exception($"No keypoints found for tour ID {tourId}.");
            }

            return Result.Ok(keyPoints);
        }
    }
}




