using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Authoring;
using Explorer.Tours.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.Authoring
{
    public class KeyPointService : CrudService<KeyPointDto, KeyPoint>, IKeyPointService
    {
        public KeyPointService(ICrudRepository<KeyPoint> repository, IMapper mapper) : base(repository, mapper) { }
    }
}
