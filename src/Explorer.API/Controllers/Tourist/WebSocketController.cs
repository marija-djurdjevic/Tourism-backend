using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Explorer.Stakeholders.Infrastructure.Database;
using Explorer.BuildingBlocks.Core.Domain;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Authorization;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Public.Administration;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Application.Services;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Core.Application.Dtos;


namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/websocket")]
    public class WebSocketController:BaseApiController
    {
        [HttpPost("register")]
        public IActionResult RegisterWebSocket()
        {
            // Dobijanje korisničkog ID-a iz tokena ili zahteva
            int userId = User.PersonId(); // Proverite da li je `ClaimsPrincipal` dostupan

            // Generisanje WebSocket URL-a
            string webSocketUrl = $"wss://{Request.Host}/ws?userId={userId}";

            return Ok(new { webSocketUrl });
        }
    }
}
