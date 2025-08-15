using Microsoft.AspNetCore.Mvc;
using Terminal.API.DTOs;
using Terminal.Dtos.Bus;
using Terminal.Dtos.Common;
using Terminal.Services.Interfaces;

namespace Terminal.API.Controllers
{
    [Route("api/buses")]
    [ApiController]
    public class BusesController : ControllerBase
    {
        private readonly IBusService _busService;

        public BusesController(IBusService busService)
        {
            _busService = busService;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDto<PaginationDto<List<BusDto>>>>> GetList(
            [FromQuery] string searchTerm = "",
            [FromQuery] string companyId = "",
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 0)
        {
            var response = await _busService.GetListAsync(
                searchTerm: searchTerm,
                companyId: companyId,
                page: page,
                pageSize: pageSize
            );

            return StatusCode((int)response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<BusActionResponse>>> GetOne(string id)
        {
            var response = await _busService.GetOneByIdAsync(id);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto<BusActionResponse>>> Create(
            [FromForm] BusCreateDto dto,
            IFormFile? image)
        {
            // Código de manejo de imagen
            if (image is not null && image.Length > 0)
            {
                var allowed = new[] { ".jpg", ".jpeg", ".png", ".webp" };
                var ext = Path.GetExtension(image.FileName).ToLowerInvariant();
                if (!allowed.Contains(ext))
                    return BadRequest("Formato de imagen no soportado.");

                var uploadsRoot = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "buses");
                Directory.CreateDirectory(uploadsRoot);

                var filename = $"{Guid.NewGuid()}{ext}";
                var fullPath = Path.Combine(uploadsRoot, filename);

                using (var stream = System.IO.File.Create(fullPath))
                    await image.CopyToAsync(stream);

                dto.ImageUrl = $"/uploads/buses/{filename}";
            }

            var response = await _busService.CreateAsync(dto);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto<BusActionResponse>>> Edit(
            [FromForm] BusCreateDto dto,
            IFormFile? image,
            string id)
        {
            // Código de manejo de imagen...
            if (image is not null && image.Length > 0)
            {
                var allowed = new[] { ".jpg", ".jpeg", ".png", ".webp" };
                var ext = Path.GetExtension(image.FileName).ToLowerInvariant();
                if (!allowed.Contains(ext))
                    return BadRequest("Formato de imagen no soportado.");

                var uploadsRoot = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "buses");
                Directory.CreateDirectory(uploadsRoot);

                var filename = $"{Guid.NewGuid()}{ext}";
                var fullPath = Path.Combine(uploadsRoot, filename);

                using (var stream = System.IO.File.Create(fullPath))
                    await image.CopyToAsync(stream);

                dto.ImageUrl = $"/uploads/buses/{filename}";
            }

            var response = await _busService.EditAsync(dto, id);

            return StatusCode((int)response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto<BusActionResponse>>> Delete(string id)
        {
            var response = await _busService.DeleteAsync(id);
            return StatusCode((int)response.StatusCode, response);
        }
    }
}