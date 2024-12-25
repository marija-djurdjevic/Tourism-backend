using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.Administration
{
    public class ClubService : CrudService<ClubDto, Club>, IClubService
    {
        private readonly ICrudRepository<Club> _repository;
        private readonly IMapper _mapper;
        public ClubService(ICrudRepository<Club> repository, IMapper mapper) : base(repository, mapper) 
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Result<List<ClubDto>> GetAll()
        {
            throw new NotImplementedException();
        }

        public ClubDto GetById(int id)
        {
            try
            {
                var club = _repository.Get(id);
                return club == null ? null : _mapper.Map<ClubDto>(club);
            }
            catch (KeyNotFoundException)
            {
                return null; // or handle appropriately
            }
        }

        public Result InviteUser(int clubId, int userId)
        {
            var club = _repository.Get(clubId);
            if (club == null)
                return Result.Fail("Club not found.");

            club.InviteUser(userId);
            _repository.Update(club);
            return Result.Ok();
        }

        public Result AcceptInvitation(int clubId, int userId)
        {
            var club = _repository.Get(clubId);
            if (club == null)
                return Result.Fail("Club not found.");

            club.AcceptInvitation(userId);
            _repository.Update(club);
            return Result.Ok();
        }

        public Result RejectInvitation(int clubId, int userId)
        {
            var club = _repository.Get(clubId);
            if (club == null)
                return Result.Fail("Club not found.");

            club.RejectInvitation(userId);
            _repository.Update(club);
            return Result.Ok();
        }

        public Result RemoveMember(int clubId, int userId)
        {
            var club = _repository.Get(clubId);
            if (club == null)
                return Result.Fail("Club not found.");

            club.RemoveMember(userId);
            _repository.Update(club);
            return Result.Ok();
        }

        public PagedResult<ClubDto> GetUserInvitations(int userId, int page, int pageSize)
        {
            // Fetch paged results from the repository
            var pagedClubs = _repository.GetPaged(page, pageSize);

            // Filter clubs where the invitationIds list contains the given userId
            var invitedClubs = pagedClubs.Results
                .Where(club => club.InvitationIds != null && club.InvitationIds.Contains(userId))
                .ToList();

            // Map the filtered entities to ClubDto
            var clubDtos = invitedClubs.Select(club => _mapper.Map<ClubDto>(club)).ToList();

            // Return the filtered results as a PagedResult
            return new PagedResult<ClubDto>(clubDtos, pagedClubs.TotalCount);
        }

        public Result RequestJoin(int clubId, int userId)
        {
            var club = _repository.Get(clubId);
            if (club == null)
                return Result.Fail("Club not found.");

            club.RequestJoin(userId);
            _repository.Update(club);
            return Result.Ok();
        }

        public Result AcceptRequest(int clubId, int userId, int personId)
        {
            var club = _repository.Get(clubId);
            if (club == null)
                return Result.Fail("Club not found.");

            if(personId != club.OwnerId)
            {
                return Result.Fail("You are not club owner");
            }
            club.AcceptRequest(userId);
            _repository.Update(club);
            return Result.Ok();
        }

        public Result DenyRequest(int clubId, int userId, int personId)
        {
            var club = _repository.Get(clubId);
            if (club == null)
                return Result.Fail("Club not found.");

            if (personId != club.OwnerId)
            {
                return Result.Fail("You are not club owner");
            }
            club.DenyRequest(userId);
            _repository.Update(club);
            return Result.Ok();
        }
    }
}
