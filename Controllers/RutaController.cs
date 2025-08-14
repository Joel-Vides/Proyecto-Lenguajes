using Microsoft.AspNetCore.Mvc;
using Terminal.Dtos.Common;
using Terminal.Dtos.Ruta;
using Terminal.Services.Interfaces;

namespace Terminal.Controllers
{
    [ApiController]
    [Route("api/rutas")]
    public class RutaController : ControllerBase
    {
        private readonly IRutaService _rutaService;

        public RutaController(IRutaService rutaService)
        {
            _rutaService = rutaService;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDto<List<RutaDto>>>> GetList(
            string searchTerm = "", int page = 1, int pageSize = 0)
        {
            var response = await _rutaService.GetListAsync(searchTerm, page, pageSize);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<RutaDto>>> GetOne(int id)
        {
            var response = await _rutaService.GetOneByIdAsync(id);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto<RutaActionResponseDto>>> Post([FromBody] RutaCreateDto dto)
        {
            var response = await _rutaService.CreateAsync(dto);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data
            });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto<RutaActionResponseDto>>> Edit([FromBody] RutaEditDto dto, int id)
        {
            var response = await _rutaService.EditAsync(dto, id);

            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto<RutaActionResponseDto>>> Delete(int id)
        {
            var response = await _rutaService.DeleteAsync(id);

            return StatusCode(response.StatusCode, response);
        }
    }
}
