using Microsoft.AspNetCore.Mvc;
using Terminal.Dtos.Common;
using Terminal.Dtos.Horarios;
using Terminal.Services.Interfaces;

namespace Terminal.API.Controllers
{
    [Route("api/horarios")]
    [ApiController]
    public class HorariosController : ControllerBase
    {
        private readonly IHorarioService _horarioService;

        public HorariosController(IHorarioService horarioService)
        {
            _horarioService = horarioService;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDto<List<HorarioActionResponseDto>>>> GetList()
        {
            var response = await _horarioService.GetListAsync();

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<HorarioActionResponseDto>>> GetOne(int id)
        {
            var response = await _horarioService.GetOneByIdAsync(id);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data
            });
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto<HorarioActionResponseDto>>> Create([FromBody] HorarioCreateDto dto)
        {
            var response = await _horarioService.CreateAsync(dto);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data
            });
        }

        [HttpPut]
        public async Task<ActionResult<ResponseDto<HorarioActionResponseDto>>> Edit([FromBody] HorarioEditDto dto)
        {
            var response = await _horarioService.EditAsync(dto);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data
            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto<HorarioActionResponseDto>>> Delete(int id)
        {
            var response = await _horarioService.DeleteAsync(id);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data
            });
        }
    }
}
