using Explorer.BuildingBlocks.Core.UseCases;

using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.TourSessionDtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Explorer.Tours.API.Public.Execution
{
    public interface ITourSessionService 
    {
        public Result<TourSessionDto> StartTour(int tourId,int userId, LocationDto initialLocation);

        public Result<int> GetMostRecentlyCompletedKeyPointId(int tourId, int userId);

        public Result<TourSessionDto> CompleteTour(int tourSessionId, int userId);

        public Result<TourSessionDto> AbandonTour(int tourSessionId, int userId);

        public bool UpdateLocation(int tourId, LocationDto locationDto, int userId);

        public void UpdateSession(int tourId, LocationDto locationDto, int userId);
        public bool CanUserReviewTour(int tourId, int userId);
        public (int,DateTime) GetProgressAndLastActivity(int tourId, int userId);
        public Result<TourSessionDto> UpdateLastActivity(int tourSessionId, int userId);
        public Result<List<CompletedKeyPointDto>> GetCompletedKeyPoints(int tourSessionId, int userId);
        public Result<TourSessionDto> AddCompletedKeyPoint(long tourId, long keyPointId, int userId);
        public Result<List<KeyPointDto>> GetKeyPointsByTourId(int tourId);

    }
}
