using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Explorer.Stakeholders.Infrastructure.Database;
using Explorer.BuildingBlocks.Core.Domain;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Authorization;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/image")]
    public class ImageController : ControllerBase
    {
        private readonly StakeholdersContext _context;

        public ImageController(StakeholdersContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                var imageModel = new Image(memoryStream.ToArray(), file.ContentType);
                _context.Images.Add(imageModel);
                await _context.SaveChangesAsync();
                return Ok(imageModel.Id);
            }
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetImage(long id)
        {
            try
            {
                var image = await _context.Images.FindAsync(id);
                if (image == null)
                    return NotFound();  // 404 ako slika nije pronađena

                return File(image.ImageData, image.ContentType);  // Vrati sliku
            }
            catch (Exception ex)
            {
                // Loguj grešku za dalje praćenje
                return StatusCode(500, "An error occurred while retrieving the image.");  // Vrati 500 u slučaju neočekivane greške
            }
        }
    }
}
