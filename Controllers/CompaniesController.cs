using Microsoft.AspNetCore.Mvc;
using Terminal.Dtos.Common;
using Terminal.Dtos.Empresa;
using Terminal.Services.Interfaces;

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
         string searchTerm = "", int page = 1, int pageSize = 0)
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
            [FromBody] CompanyCreateDto dto)
        {
            var response = await _companyService.CreateAsync(dto);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto<CompanyActionResponseDto>>> Update(
            [FromBody] CompanyDto dto,
            [FromRoute] string id)
        {
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