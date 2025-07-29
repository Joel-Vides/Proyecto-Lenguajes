using Terminal.Dtos.Bus;
using Terminal.Dtos.Common;

namespace Terminal.Services.Interfaces
{
    public interface IBusService
    {
        Task<ResponseDto<List<BusActionResponse>>> GetListAsync();
        Task<ResponseDto<BusActionResponse>> GetOneByIdAsync(string id);
        Task<ResponseDto<BusActionResponse>> CreateAsync(BusCreateDto dto);
        Task<ResponseDto<BusActionResponse>> EditAsync(BusCreateDto dto, string id);
        Task<ResponseDto<BusActionResponse>> DeleteAsync(string id);
    }
}
