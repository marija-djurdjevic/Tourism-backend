﻿using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos.EncounterDtos;
using Explorer.Encounters.API.Dtos.SecretsDtos;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.Domain.Encounters;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using Explorer.Encounters.Core.Domain.Secrets;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.UseCases
{
    public class StoryService : CrudService<StoryDto, Story>, IStoryService
    {
        private readonly ICrudRepository<Story> _repository;
        private readonly IStoryRepository _storyRepository;
        private readonly IMapper _mapper;

        public StoryService(ICrudRepository<Story> repository, IMapper mapper, IStoryRepository storyRepository) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _storyRepository = storyRepository;
        }

        public Result<StoryDto> GetById(int storyId)
        {
           
            return MapToDto(_storyRepository.GetById(storyId));
        }


        public Result<List<StoryDto>> GetByBookId(int storyId)
        {

            return MapToDto(_storyRepository.GetByBookId(storyId));
        }

        public Result<StoryDto> StoryStatusSet(StoryDto sstory)
        {
            sstory.StoryStatus = API.Dtos.SecretsDtos.StoryStatus.Declined;
            Update(sstory);

            return Result.Ok(sstory);

        }

    }
}
