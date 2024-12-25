using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.Administration
{
    public interface IClubService
    {
        Result<PagedResult<ClubDto>> GetPaged(int page, int pageSize);
        Result<ClubDto> Create(ClubDto club);
        Result<ClubDto> Update(ClubDto club);
        Result Delete(int id);
        Result<List<ClubDto>> GetAll();
        ClubDto GetById(int id);
        Result InviteUser(int clubId, int userId);
        Result AcceptInvitation(int clubId, int userId);
        Result RejectInvitation(int clubId, int userId);
        Result RemoveMember(int clubId, int userId);
        PagedResult<ClubDto> GetUserInvitations(int userId, int page, int pageSize);
        Result AcceptRequest(int clubId, int userId, int personId);
        Result DenyRequest(int clubId, int userId, int personId);
        Result RequestJoin(int clubId, int userId);
    }
}
