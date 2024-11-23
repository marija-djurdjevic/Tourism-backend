using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Explorer.Tours.API.Public.Administration
{
    public interface IObjectService
    {
        Result<PagedResult<ObjectDto>> GetPaged(int page, int pageSize);

        Result<ObjectDto> Create(ObjectDto objectDto);

        Result<ObjectDto> Get(int id); 

        Result<ObjectDto> Update( ObjectDto objectDto);
        Result<ObjectDto> PublishObject(int id);

        Result Delete(int id);
    }
}
