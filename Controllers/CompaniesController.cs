using Microsoft.AspNetCore.Mvc;
using Terminal.Dtos.Common;
using Terminal.Dtos.Empresa;
using Terminal.Services.Interfaces;
using static System.Net.Mime.MediaTypeNames;

namespace Terminal.Controllers
{
    [ApiController]
    [Route("api/companies")]
    public class EmpresasController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public EmpresasController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDto<PaginationDto<List<CompanyDto>>>>> GetList(
            string searchTerm = "", int page = 1, int pageSize = 10) 
        {
            var response = await _companyService.GetListAsync(searchTerm, page, pageSize);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<CompanyDto>>> GetOne(string id)
        {
            var response = await _companyService.GetOneByIdAsync(id);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto<CompanyActionResponseDto>>> Create(
            [FromForm] CompanyCreateDto dto, IFormFile image)
        {
            // Para la Imagen
            if (image is not null && image.Length > 0)
            {
                var allowed = new[] { ".jpg", ".jpeg", ".png", ".webp" };
                var ext = Path.GetExtension(image.FileName).ToLowerInvariant();
                if (!allowed.Contains(ext))
                    return BadRequest("Formato de imagen no soportado.");

                var uploadsRoot = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "companies");
                Directory.CreateDirectory(uploadsRoot);

                var filename = $"{Guid.NewGuid()}{ext}";
                var fullPath = Path.Combine(uploadsRoot, filename);

                using (var stream = System.IO.File.Create(fullPath))
                    await image.CopyToAsync(stream);

                dto.ImageUrl = $"/uploads/companies/{filename}";
            }

            var response = await _companyService.CreateAsync(dto);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto<List<CompanyActionResponseDto>>>> Update(
            [FromForm] CompanyEditDto dto, [FromRoute] string id, IFormFile image)
        {
            // Para La imgen del Bus
            if (image is not null && image.Length > 0)
            {
                var allowed = new[] { ".jpg", ".jpeg", ".png", ".webp" };
                var ext = Path.GetExtension(image.FileName).ToLowerInvariant();
                if (!allowed.Contains(ext))
                    return BadRequest("Formato de imagen no soportado.");

                var uploadsRoot = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "companies");
                Directory.CreateDirectory(uploadsRoot);

                var filename = $"{Guid.NewGuid()}{ext}";
                var fullPath = Path.Combine(uploadsRoot, filename);

                using (var stream = System.IO.File.Create(fullPath))
                    await image.CopyToAsync(stream);

                dto.ImageUrl = $"/uploads/companies/{filename}";
            }

            var response = await _companyService.UpdateAsync(id, dto);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto<CompanyActionResponseDto>>> Delete(string id)
        {
            var response = await _companyService.DeleteAsync(id);
            return StatusCode((int)response.StatusCode, response);
        }
    }
}