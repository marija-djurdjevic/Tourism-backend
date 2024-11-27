using AutoMapper;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.TourLifecycleDtos;
using Explorer.Tours.API.Dtos.TourProblemDtos;
using Explorer.Tours.API.Public.Authoring;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.Authoring
{
    public class TourService : CrudService<TourDto, Tour>, ITourService

    {

        private readonly ITourRepository _tourRepository;
        private readonly ICrudRepository<KeyPoint> _keyPointRepository;
        private readonly IMapper _mapper;
        
        public TourService(ICrudRepository<Tour> repository, IMapper mapper, ITourRepository tourRepository, ICrudRepository<KeyPoint> keyPointRepository) : base(repository, mapper)
        {
            _mapper = mapper;
            _tourRepository = tourRepository;
            _keyPointRepository = keyPointRepository;
         
        }

        public Result<List<TourDto>> GetAllPublished(int page, int pageSize)
        {
            var tours = GetPaged(page, pageSize);
            var publishedTours = tours.Value.Results.FindAll(x => x.Status == TourDto.TourStatus.Published);
            return publishedTours;

        }

        public Result<KeyPointDto> AddKeyPointToTourAsync(int tourId, KeyPointDto keyPointDto)
        {
            try
            {

                var tour = GetTourByIdAsync(tourId);

                if (tour == null)
                {
                    return Result.Fail<KeyPointDto>("Tour not found.");
                }
                Console.WriteLine($"Tour found: {tour.Id}, KeyPoints Count: {tour.KeyPoints.Count}");

                var keyPoint = _mapper.Map<KeyPoint>(keyPointDto);


                tour.KeyPoints.Add(keyPoint);

                _keyPointRepository.Create(keyPoint);



                var tourDto = _mapper.Map<TourDto>(tour);
                Update(tourDto);


                return Result.Ok(_mapper.Map<KeyPointDto>(keyPoint));
            }
            catch (Exception ex)
            {
                var innerExceptionMessage = ex.InnerException?.Message ?? "No inner exception.";
                return Result.Fail<KeyPointDto>("An error occurred while adding the key point: " + ex.Message);
            }
        }

        private Tour GetTourByIdAsync(int tourId)
        {
            return _tourRepository.GetByIdAsync(tourId);
        }

        public Result<PagedResult<TourDto>> GetByAuthorId(int page, int pageSize, int id)
        {
            var tours = GetPaged(page, pageSize);
            var filteredResults = tours.Value.Results
                .Where(x => x.AuthorId == id)
                .ToList();

            var pagedAuthorTours = new PagedResult<TourDto>(filteredResults, filteredResults.Count);

            return tours.WithValue(pagedAuthorTours);
        }

        public Result<TourDto> Publish(TourDto tourDto)
        {
            try
            {
                var tour = _mapper.Map<Tour>(tourDto);
                PagedResult<KeyPoint> pagedKeypoints = _keyPointRepository.GetPaged(1, 10);
                var keypoints = pagedKeypoints.Results.FindAll(x => x.TourIds.Contains(tour.Id)).ToList();
                foreach (var kp in keypoints)
                {
                    _tourRepository.Detach(kp);
                    tour.KeyPoints.Add(kp);
                }
                tour.Publish();
                var updatedTourDto = _mapper.Map<TourDto>(tour);
                Update(updatedTourDto);

                return Result.Ok(updatedTourDto);
            }
            catch (Exception ex)
            {
                return Result.Fail("An error occurred while publishing the tour: " + ex.Message);
            }
        }

        public Result<TourDto> Archive(TourDto tourDto)
        {
            try
            {
                var tour = _mapper.Map<Tour>(tourDto);
                tour.Archive();
                var updatedTourDto = _mapper.Map<TourDto>(tour);
                Update(updatedTourDto);

                return Result.Ok(updatedTourDto);
            }
            catch (Exception ex)
            {
                return Result.Fail("An error occurred while publishing the tour: " + ex.Message);
            }
        }

        public Result<TourDto> Close(TourDto tourDto)
        {
            try
            {
                var tour = _mapper.Map<Tour>(tourDto);
                tour.CLose();
                var updatedTourDto = _mapper.Map<TourDto>(tour);
                Update(updatedTourDto);

                return Result.Ok(updatedTourDto);
            }
            catch (Exception ex)
            {
                return Result.Fail("An error occurred while closing the tour: " + ex.Message);
            }
        }

        public Result<List<TourDto>> GetAllToursWithKeyPoints()
        {

            var tours = _tourRepository.GetAllToursWithKeyPoints();
          

            if (tours == null || !tours.Any())
            {
                return Result.Fail<List<TourDto>>("No tours found.");
            }

            var tourDtos = tours.Select(t => _mapper.Map<TourDto>(t)).ToList();

            return Result.Ok(tourDtos);
        }



        /*public Result<List<KeyPointDto>> GetKeyPointsByTourId(int tourId)

        {

             var pagedTours = GetPaged(1, int.MaxValue); 

             if (pagedTours.IsFailed)
             {
                 return Result.Fail<List<KeyPointDto>>("Failed to retrieve tours.");
             }

             var tour = pagedTours.Value.Results.FirstOrDefault(x => x.Id == tourId);

             if (tour == null)
             {
                 return Result.Fail<List<KeyPointDto>>($"Tour with ID {tourId} not found.");
             }


             return tour.KeyPoints;
        }
        */


        public Result<TourDto> Get(int tourId)
        {

            var tour = _tourRepository.GetTourWithKeyPoints(tourId);

            if (tour == null)
            {
                return Result.Fail<TourDto>($"Tour with ID {tourId} not found.");
            }


            var tourDto = _mapper.Map<TourDto>(tour);
            return Result.Ok(tourDto);
        }

        public Result<TourDto> GetById(int tourId)
        {
            var tour = _tourRepository.GetById(tourId);
            if (tour == null)
            {
                return Result.Fail("Tour not found");
            }


            var tourDto = _mapper.Map<TourDto>(tour);
            return Result.Ok(tourDto);
        }


        public TourDto GetKeyPointsByTourId(int tourId)
        {

            var tour = _tourRepository.GetKeyPointsForTour(tourId);
            var tourDto = _mapper.Map<TourDto>(tour);
            //return Result.Ok(tourDto);
            return tourDto;
        }

        public Result<List<TourDto>> SearchTours(SearchByDistanceDto searchByDistance)
        {
            var tours = _tourRepository.GetAllToursWithKeyPoints();

            var keyPoints = _keyPointRepository.GetPaged(0, 0);
            foreach (Tour tour in tours)
            {
                foreach (KeyPoint kp in keyPoints.Results)
                {
                    if (kp.TourIds.Contains(tour.Id))
                    {
                        tour.KeyPoints.Add(kp);
                    }
                }
            }
            List<TourDto> matchingTours = new List<TourDto>();
            var coordinate = new Coordinates(searchByDistance.Latitude, searchByDistance.Longitude);
            foreach (var t in tours)
            {
                if (t.Status == TourStatus.Published && t.HasKeyPointsInDesiredDistance(coordinate, searchByDistance.Distance))
                    matchingTours.Add(MapToDto(t));
            }
            Result<List<TourDto>> result = new Result<List<TourDto>>();
            result.WithValue(matchingTours);
            return result;
        }


        public Result<bool> UpdateTransportInfo(int tourId, TransportInfoDto transportInfoDto)
        {

            var tourDto = GetKeyPointsByTourId(tourId);
            var tour = _mapper.Map<Tour>(tourDto);

            if (tour == null)
            {
                return Result.Fail<bool>("Tour not found");
            }

            tour.UpdateTrasnportStatus(transportInfoDto.Distance, transportInfoDto.Time);



            var updatedTourDto = _mapper.Map<TourDto>(tour);
            Update(updatedTourDto);

            return Result.Ok(true);
        }

    }
}
