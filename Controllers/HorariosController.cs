using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Terminal.Constants;
using Terminal.Dtos.Common;
using Terminal.Dtos.Horarios;
using Terminal.Services.Interfaces;

namespace Terminal.API.Controllers
{
    [Route("api/horarios")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class HorariosController : ControllerBase
    {
        private readonly IHorarioService _horarioService;

        public HorariosController(IHorarioService horarioService)
        {
            _horarioService = horarioService;
        }

        [HttpGet]
        [Authorize(Roles = $"{RolesConstant.SYS_ADMIN}, {RolesConstant.NORMAL_USER}")]
        public async Task<ActionResult<ResponseDto<List<HorarioActionResponseDto>>>> GetList()
        {
            var response = await _horarioService.GetListAsync();

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = $"{RolesConstant.SYS_ADMIN}, {RolesConstant.NORMAL_USER}")]
        public async Task<ActionResult<ResponseDto<HorarioActionResponseDto>>> GetOne(int id)
        {
            var response = await _horarioService.GetOneByIdAsync(id);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        [Authorize(Roles = $"{RolesConstant.SYS_ADMIN}")]
        public async Task<ActionResult<ResponseDto<HorarioActionResponseDto>>> Create([FromBody] HorarioCreateDto dto)
        {
            var response = await _horarioService.CreateAsync(dto);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = $"{RolesConstant.SYS_ADMIN}")]
        public async Task<ActionResult<ResponseDto<HorarioActionResponseDto>>> Edit([FromBody] HorarioEditDto dto, int id)
        {
            dto.Id = id;
            var response = await _horarioService.EditAsync(dto);

            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = $"{RolesConstant.SYS_ADMIN}")]
        public async Task<ActionResult<ResponseDto<HorarioActionResponseDto>>> Delete(int id)
        {
            var response = await _horarioService.DeleteAsync(id);

            return StatusCode(response.StatusCode, response);
        }
    }
}