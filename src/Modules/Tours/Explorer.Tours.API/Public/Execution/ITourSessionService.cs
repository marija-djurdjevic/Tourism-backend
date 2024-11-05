using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.Core.Domain.Users;
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
        public Result<TourSessionDto> StartTour(int tourId, LocationDto initialLocation,int touristId);

        public Result<TourSessionDto> CompleteTour(int tourSessionId);

        public Result<TourSessionDto> AbandonTour(int tourSessionId);

        public bool UpdateLocation(int tourId, LocationDto locationDto);

        public void UpdateSession(int tourId, LocationDto locationDto);

    }
}
