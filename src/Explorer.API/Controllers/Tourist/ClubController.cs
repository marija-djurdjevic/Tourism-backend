using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.UseCases.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/administration/club")]
    public class ClubController : BaseApiController
    {
        private readonly IClubService _clubService;

        public ClubController(IClubService clubService)
        {
            _clubService = clubService;
        }

        [HttpGet]
        public ActionResult<PagedResult<ClubDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _clubService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<ClubDto> Create([FromBody] ClubDto clubDto)
        {
            clubDto.OwnerId = User.PersonId(); 
            var result = _clubService.Create(clubDto);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<ClubDto> Update([FromBody] ClubDto club)
        {
            if (club.OwnerId != User.PersonId())
            {
                return Forbid();
            } 
            var result = _clubService.Update(club);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var club = _clubService.GetById(id);

            // Check if the club exists
            if (club == null)
            {
                return NotFound(); // Return 404 if the club doesn't exist
            }

            // Check if the logged-in user is the owner of the club
            if (club.OwnerId != User.PersonId())
            {
                return Forbid();
            }

            var result = _clubService.Delete(id);
            return CreateResponse(result);
        }

        [HttpPost("{clubId}/invite")]
        public ActionResult InviteUser(int clubId, [FromBody] int userId)
        {
            var result = _clubService.InviteUser(clubId, userId);
            if (result.IsSuccess)
                return Ok();
            return BadRequest(result);
        }

        [HttpPost("{clubId}/accept")]
        public ActionResult AcceptInvitation(int clubId)
        {
            int userId = User.PersonId();
            var result = _clubService.AcceptInvitation(clubId, userId);
            if (result.IsSuccess)
                return Ok();
            return BadRequest(result);
        }

        [HttpPost("{clubId}/reject")]
        public ActionResult RejectInvitation(int clubId)
        {
            int userId = User.PersonId();
            var result = _clubService.RejectInvitation(clubId, userId);
            if (result.IsSuccess)
                return Ok();
            return BadRequest(result);
        }

        [HttpPost("{clubId}/remove")]
        public ActionResult RemoveMember(int clubId, [FromBody] int userId)
        {
            var result = _clubService.RemoveMember(clubId, userId);
            if (result.IsSuccess)
                return Ok();
            return BadRequest(result);
        }

        [HttpGet("invitations")]
        public ActionResult GetInvitations([FromQuery] int page, [FromQuery] int pageSize)
        {
            int userId = User.PersonId();
            var result = _clubService.GetUserInvitations(userId, page, pageSize);
            if (result.Results.Any())
                return Ok(result);

            return NotFound("No invitations found for the user.");
        }

        [HttpPost("{clubId}/request")]
        public ActionResult RequestJoin(int clubId)
        {
            int userId = User.PersonId();
            var result = _clubService.RequestJoin(clubId, userId);
            if (result.IsSuccess)
                return Ok();
            return BadRequest(result);
        }

        [HttpPost("{clubId}/acceptRequest")]
        public ActionResult AcceptRequest(int clubId, [FromBody] int userId)
        {
            int persona = User.PersonId();//jer ovo treba moci samo clubOwner
            var result = _clubService.AcceptRequest(clubId, userId, persona);
            if (result.IsSuccess)
                return Ok();
            return BadRequest(result);
        }

        [HttpPost("{clubId}/denyRequest")]
        public ActionResult DenyRequest(int clubId, [FromBody] int userId)
        {
            int persona = User.PersonId();//jer ovo treba moci samo clubOwner
            var result = _clubService.DenyRequest(clubId, userId, persona);
            if (result.IsSuccess)
                return Ok();
            return BadRequest(result);
        }
    }
}
