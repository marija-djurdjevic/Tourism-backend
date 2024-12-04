using AutoMapper;
using AutoMapper.Internal;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.Core.Domain.Users;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.PublishRequestDtos;
using Explorer.Tours.API.Dtos.TourProblemDtos;
using Explorer.Tours.API.Public.Authoring;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.PublishRequests;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.TourProblems;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.Authoring
{
    public class PublishRequestService : CrudService<PublishRequestDto, PublishRequest>, IPublishRequestService
    {
        private readonly IMapper _mapper;
        private readonly ICrudRepository<PublishRequest> _repository;

        public PublishRequestService(ICrudRepository<PublishRequest> repository, IMapper mapper) : base(repository,mapper)
        {

            _repository = repository;
            _mapper = mapper;
        }

        public Result<PublishRequestDto> Create(PublishRequestDto publishRequestDto)
        {
            //PublishRequestDto requestDto = MapToDto(request);
            publishRequestDto.Status = PublishRequestDto.RegistrationRequestStatus.Pending;
           // int authorId = User.PersonId();
            var publishRequest = _repository.Create(MapToDomain(publishRequestDto));
            return Result.Ok(MapToDto(publishRequest));

        }
    }
}
