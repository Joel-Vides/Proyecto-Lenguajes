using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Terminal.API.Services.Interfaces;
using Terminal.Constants;
using Terminal.Dtos.Common;
using Terminal.Dtos.Ticket;

namespace Terminal.API.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/tickets")]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpGet]
        [Authorize(Roles = $"{RolesConstant.SYS_ADMIN}")]
        public async Task<ActionResult<ResponseDto<List<TicketDto>>>> GetList(
            string searchTerm = "", int page = 1, int pageSize = 0
        )
        {
            var response = await _ticketService.GetListAsync(searchTerm, page, pageSize);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = $"{RolesConstant.SYS_ADMIN}, {RolesConstant.NORMAL_USER}")]
        public async Task<ActionResult<ResponseDto<TicketDto>>> GetOne(string id)
        {
            var response = await _ticketService.GetOneByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        [Authorize(Roles = $"{RolesConstant.SYS_ADMIN}, {RolesConstant.NORMAL_USER}")]
        public async Task<ActionResult<ResponseDto<TicketActionResponseDto>>> Post([FromBody] TicketCreateDto dto)
        {
            var response = await _ticketService.CreateAsync(dto);
            return StatusCode(response.StatusCode, response);
        }

        //[HttpDelete("{id}")]
        //public async Task<ActionResult<ResponseDto<TicketActionResponseDto>>> Delete(string id)
        //{
        //    var response = await _ticketService.DeleteAsync(id);
        //    return StatusCode(response.StatusCode, response);
        //}
    }
}