using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.Administration
{
    public class ObjectService : CrudService<ObjectDto, Explorer.Tours.Core.Domain.Object>, IObjectService
    {
        private readonly IMapper _mapper;

        public ObjectService(ICrudRepository<Explorer.Tours.Core.Domain.Object> repository, IMapper mapper) : base(repository, mapper) {
            _mapper = mapper;
        }

        public Result<ObjectDto> GetById(int id)
        {
            
            var entity = base.CrudRepository.Get(id);

          
            if (entity == null)
            {
                return Result.Fail<ObjectDto>("Object not found");
            }
          
            var dto = _mapper.Map<ObjectDto>(entity);

            return Result.Ok(dto);
        }

        public Result<ObjectDto> PublishObject(int id, int flag)
        {
            
            var obj = base.CrudRepository.Get(id);
            if (obj == null)
                return Result.Fail("Object not found");

            if (flag == 0) obj.UpdateObjectStatus(ObjectStatus.Public);
            if (flag == 1) obj.UpdateObjectStatus(ObjectStatus.Rejected);
            base.CrudRepository.Update(obj);
            return Result.Ok(MapToDto(obj));
        }

    }
}
