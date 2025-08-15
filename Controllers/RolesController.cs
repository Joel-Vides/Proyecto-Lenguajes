using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Terminal.Constants;
using Terminal.Dtos.Common;
using Terminal.Dtos.Security.Roles;
using Terminal.Services.Interfaces;

namespace Terminal.Controllers
{
    [Route("api/roles")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class RolesController : ControllerBase
    {
        private readonly IRolesService _rolesService;

        public RolesController(IRolesService rolesService)
        {
            _rolesService = rolesService;
        }

        [HttpGet]
        [Authorize(Roles = $"{RolesConstant.SYS_ADMIN}")]
        public async Task<ActionResult<ResponseDto<PaginationDto
            <List<RoleDto>>>>> GetListPagination(
            string searchTerm = "", int page = 1, int pageSize = 10
            )
        {
            var response = await _rolesService
                    .GetListAsync(searchTerm, page, pageSize);

            return StatusCode(response.StatusCode,
                new ResponseDto<PaginationDto<List<RoleDto>>>
                {
                    Status = response.Status,
                    Message = response.Message,
                    Data = response.Data
                });
        }

        [HttpGet("{id}")]
        [Authorize(Roles = $"{RolesConstant.SYS_ADMIN}")]
        public async Task<ActionResult<ResponseDto<RoleDto>>> GetOneById(string id)
        {
            var response = await _rolesService.GetOneById(id);
            return StatusCode(response.StatusCode,
                new ResponseDto<RoleDto>
                {
                    Status = response.Status,
                    Message = response.Message,
                    Data = response.Data
                });
        }

        [HttpPost]
        [Authorize(Roles = $"{RolesConstant.SYS_ADMIN}")]
        public async Task<ActionResult<ResponseDto<RoleActionResponseDto>>>
            CreateAsync(
            RoleCreateDto dto
        )
        {
            var response = await _rolesService.CreateAsync(dto);
            return StatusCode(response.StatusCode,
                new ResponseDto<RoleActionResponseDto>
                {
                    Status = response.Status,
                    Message = response.Message,
                    Data = response.Data
                });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = $"{RolesConstant.SYS_ADMIN}")]
        public async Task<ActionResult<ResponseDto<RoleActionResponseDto>>> Edit(
            [FromBody] RoleEditDto dto, string id
            )
        {
            var response = await _rolesService.EditAsync(dto, id);
            return StatusCode(response.StatusCode,
                new ResponseDto<RoleActionResponseDto>
                {
                    Status = response.Status,
                    Message = response.Message,
                    Data = response.Data
                });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = $"{RolesConstant.SYS_ADMIN}")]
        public async Task<ActionResult<ResponseDto<RoleActionResponseDto>>> Delete(string id)
        {
            var response = await _rolesService.DeleteAsync(id);
            return StatusCode(response.StatusCode,
                new ResponseDto<RoleActionResponseDto>
                {
                    Status = response.Status,
                    Message = response.Message,
                    Data = response.Data
                });
        }
    }
}
