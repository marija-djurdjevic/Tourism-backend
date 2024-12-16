using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.GroupTourDtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.Authoring
{
    public interface IGroupTourExecutionService
    {
        Result<PagedResult<GroupTourExecutionDto>> GetPaged(int page, int pageSize);
        Result<GroupTourExecutionDto> Create(GroupTourExecutionDto groupTourExecution);
    }
}
