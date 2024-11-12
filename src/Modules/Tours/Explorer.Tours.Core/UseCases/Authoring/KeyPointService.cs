using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Authoring;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;
using System.Collections.Generic;
using System.Linq;

namespace Explorer.Tours.Core.UseCases.Administration
{
    public class KeyPointService : CrudService<KeyPointDto, KeyPoint>, IKeyPointService
    {

        private readonly ICrudRepository<KeyPoint> _repository;
        private readonly IMapper _mapper;

        public KeyPointService(ICrudRepository<KeyPoint> repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public Result<List<KeyPointDto>> GetKeyPointsByTourId(int tourId)
        {
           
            var pagedResult = _repository.GetPaged(1, int.MaxValue);

            
            if (pagedResult == null || pagedResult.Results == null || !pagedResult.Results.Any())
            {
                return Result.Fail<List<KeyPointDto>>("No key points found for this tour.");
            }

           
            var keyPoints = pagedResult.Results.Where(kp => kp.TourId == tourId).ToList();

            
            var keyPointDtos = _mapper.Map<List<KeyPointDto>>(keyPoints);
            return Result.Ok(keyPointDtos);
        }
    }
}
