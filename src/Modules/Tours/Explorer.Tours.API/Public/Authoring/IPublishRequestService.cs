using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos.PublishRequestDtos;
using Explorer.Tours.API.Dtos.TourLifecycleDtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.Authoring
{
    public interface IPublishRequestService
    {

        Result<PagedResult<PublishRequestDto>> GetPaged(int page, int pageSize);

        Result<PublishRequestDto> Create(PublishRequestDto publishRequest);

        Result<PublishRequestDto> Update(PublishRequestDto publishRequest);
    }
}
