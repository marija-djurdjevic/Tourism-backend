using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Tours.API.Dtos.TourLifeCycleDtos;
using Explorer.Tours.API.Public.Execution;
using Explorer.Tours.Core.Domain.Tours;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.BuildingBlocks.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Tours.Core.UseCases.Administration
{
    public class TourReviewService : CrudService<TourReviewDto, TourReview>, ITourReviewService
    {
        private readonly ITourSessionService _sessionService;

        public TourReviewService(ICrudRepository<TourReview> repository, ITourSessionService tourSessionService,
            IMapper mapper) : base(repository, mapper)
        {
            _sessionService = tourSessionService;
        }
        public override Result<TourReviewDto> Create(TourReviewDto tourReview)
        {
            (tourReview.TourProgressPercentage, tourReview.TourVisitDate) = _sessionService.GetProgressAndLastActivity(tourReview.TourId, tourReview.UserId);
            var canCreate = _sessionService.CanUserReviewTour(tourReview.TourId, tourReview.UserId);
            var existingReview = Get(tourReview.TourId, tourReview.UserId).Value;
            
            tourReview.Id = existingReview?.Id ?? 0;

            if (IsTourReviewedByTourist(tourReview.UserId, tourReview.TourId))
            {
                if (canCreate)
                {
                    base.Delete(existingReview.Id);
                    return base.Create(tourReview);
                }
                throw new InvalidOperationException("Već ste ostavili review za ovaj tur.");
            }
            if (!canCreate)
            {
                throw new InvalidOperationException("Ne može se kreirati review zbog zadatog uslova.");
            }
            return base.Create(tourReview);
        }

        public bool IsTourReviewedByTourist(int userId, int tourId)
        {
            return GetPaged(0, 0).Value.Results.Any(x => x.UserId == userId && x.TourId == tourId);
        }

        public Result<TourReviewDto> Get(int tourId, int userId)
        {
            return GetPaged(0, 0).Value.Results.FirstOrDefault(x => x.UserId == userId && x.TourId == tourId);
        }
    }
}
