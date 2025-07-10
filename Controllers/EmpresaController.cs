using Microsoft.AspNetCore.Mvc;
using Terminal.Dtos.Common;
using Terminal.Dtos.Empresa;
using Terminal.Services.Interfaces;

namespace Terminal.Controllers
{
    [ApiController]
    [Route("api/empresas")]
    public class EmpresasController : ControllerBase
    {
        private readonly IEmpresaService _empresaService;

        public EmpresasController(IEmpresaService empresaService)
        {
            _empresaService = empresaService;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDto<PaginationDto<List<EmpresaDto>>>>> GetList(
         string searchTerm = "", int page = 1, int pageSize = 0)
        {
            var response = await _empresaService.GetListAsync(searchTerm, page, pageSize);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<EmpresaDto>>> GetOne(string id)
        {
            var response = await _empresaService.GetOneByIdAsync(id);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto<EmpresaActionResponseDto>>> Create(
            [FromBody] EmpresaCreateDto dto)
        {
            var response = await _empresaService.CreateAsync(dto);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto<EmpresaActionResponseDto>>> Update(
            [FromBody] EmpresaDto dto,
            [FromRoute] string id)
        {
            var response = await _empresaService.UpdateAsync(id, dto);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto<EmpresaActionResponseDto>>> Delete(string id)
        {
            var response = await _empresaService.DeleteAsync(id);
            return StatusCode((int)response.StatusCode, response);
        }
    }
}