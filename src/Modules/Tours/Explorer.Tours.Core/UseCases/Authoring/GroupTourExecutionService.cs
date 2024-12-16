using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.GroupTourDtos;
using Explorer.Tours.API.Public.Authoring;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.GroupTours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.Authoring
{
    public class GroupTourExecutionService : CrudService<GroupTourExecutionDto, GroupTourExecution>, IGroupTourExecutionService
    {
        public GroupTourExecutionService(ICrudRepository<GroupTourExecution> repository, IMapper mapper) : base(repository, mapper) { }
    }
}
