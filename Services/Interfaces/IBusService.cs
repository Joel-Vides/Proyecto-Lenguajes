using Terminal.API.DTOs;
using Terminal.Dtos.Bus;
using Terminal.Dtos.Common;

namespace Terminal.Services.Interfaces
{
    public interface IBusService
    {
        Task<ResponseDto<BusActionResponse>> GetOneByIdAsync(string id);
        Task<ResponseDto<BusActionResponse>> CreateAsync(BusCreateDto dto);
        Task<ResponseDto<BusActionResponse>> EditAsync(BusCreateDto dto, string id);
        Task<ResponseDto<BusActionResponse>> DeleteAsync(string id);
        Task<ResponseDto<PaginationDto<List<BusDto>>>> GetListAsync(string searchTerm = "", int page = 1, int pageSize = 0);
    }
}
