using Microsoft.AspNetCore.Mvc;
using Terminal.Dtos.Bus;
using Terminal.Dtos.Common;
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
        public async Task<ActionResult<ResponseDto<List<BusActionResponse>>>> GetList()
        {
            var response = await _busService.GetListAsync();

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<BusActionResponse>>> GetOne(Guid id)
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
        public async Task<ActionResult<ResponseDto<BusActionResponse>>> Edit([FromBody] BusCreateDto dto, Guid id)
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
        public async Task<ActionResult<ResponseDto<BusActionResponse>>> Delete(Guid id)
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
