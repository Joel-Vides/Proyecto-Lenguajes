using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Terminal.API.DTOs;
using Terminal.Dtos.Bus;
using Terminal.Dtos.Common;
using Terminal.Dtos.Empresa;
using Terminal.Services;
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
        public async Task<ActionResult<ResponseDto<PaginationDto<List<BusDto>>>>> GetList(string searchTerm = "", int page = 1, int pageSize = 0)
        {
            var response = await _busService.GetListAsync(searchTerm, page, pageSize);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<BusActionResponse>>> GetOne(string id)
        {
            var response = await _busService.GetOneByIdAsync(id);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data
            });
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto<BusActionResponse>>> Create([FromBody] BusCreateDto dto)
        {
            var response = await _busService.CreateAsync(dto);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data
            });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto<BusActionResponse>>> Edit([FromBody] BusCreateDto dto, string id)
        {
            var response = await _busService.EditAsync(dto, id);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data
            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto<BusActionResponse>>> Delete(string id)
        {
            var response = await _busService.DeleteAsync(id);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data
            });
        }
    }
}
