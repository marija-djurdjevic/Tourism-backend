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

            // Check if the logged-in user is the owner of the club
            if (club?.OwnerId != User.PersonId())
            {
                // Return Forbidden response if the user is not the owner
                return Forbid();  // Or Unauthorized() depending on the use case
            }
            var result = _clubService.Delete(id);
            return CreateResponse(result);
        }
    }
}
