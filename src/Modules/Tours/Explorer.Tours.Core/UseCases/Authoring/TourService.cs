using AutoMapper;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
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
        private readonly IMapper _mapper;
        public TourService(ICrudRepository<Tour> repository, IMapper mapper) : base(repository, mapper) {
            _mapper = mapper;
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
    }
}
