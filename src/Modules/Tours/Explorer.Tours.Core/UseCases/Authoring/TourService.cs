using AutoMapper;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.TourLifecycleDtos;
using Explorer.Tours.API.Public.Authoring;
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
        private readonly IMapper _mapper;
        public TourService(ICrudRepository<Tour> repository, IMapper mapper, ITourRepository tourRepository) : base(repository, mapper) {          
            _mapper = mapper;
            _tourRepository = tourRepository;
        }

        public Result<List<TourDto>> GetByAuthorId(int page, int pageSize, int id)
        {
            var tours = GetPaged(page, pageSize);
            var authorTours = tours.Value.Results.FindAll(x => x.AuthorId == id);
            return authorTours;
        }

        public Result<bool> Publish(TourDto tourDto)
        {
            try
            {
                var tour = _mapper.Map<Tour>(tourDto);
                tour.Publish();
                var updatedTourDto = _mapper.Map<TourDto>(tour);
                Update(updatedTourDto);

                return Result.Ok(true);
            }
            catch (Exception ex)
            {
                return Result.Fail("An error occurred while publishing the tour: " + ex.Message);
            }
        }

        public Result<bool> Archive(TourDto tourDto)
        {
            throw new NotImplementedException();
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
        public Result<List<KeyPointDto>> GetKeyPointsByTourId(int tourId)
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





    }
}
