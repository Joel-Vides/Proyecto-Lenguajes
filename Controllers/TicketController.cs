using Microsoft.AspNetCore.Mvc;
using Terminal.API.Dtos.Tickets;
using Terminal.API.Services.Interfaces;
using Terminal.Dtos.Common;
using Terminal.Dtos.Ticket;
using Terminal.Services.Interfaces;

namespace Terminal.API.Controllers
{
    [ApiController]
    [Route("api/tickets")]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDto<List<TicketDto>>>> GetList(
            string searchTerm = "", int page = 1, int pageSize = 0
        )
        {
            var response = await _ticketService.GetListAsync(searchTerm, page, pageSize);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<TicketDto>>> GetOne(string id)
        {
            var response = await _ticketService.GetOneByIdAsync(id);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto<TicketActionResponseDto>>> Post([FromBody] TicketCreateDto dto)
        {
            var response = await _ticketService.CreateAsync(dto);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data,
            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto<TicketActionResponseDto>>> Delete(string id)
        {
            var response = await _ticketService.DeleteAsync(id);

            return StatusCode(response.StatusCode, response);
        }
    }
}
