using Terminal.Dtos.Common;
using Terminal.Dtos.Ruta;

namespace Terminal.Services.Interfaces
{
    public interface IRutaService
    {
        Task<ResponseDto<PaginationDto<List<RutaDto>>>> GetListAsync(string searchTerm = "", int page = 1, int pageSize = 0);
        Task<ResponseDto<RutaDto>> GetOneByIdAsync(int id);
        Task<ResponseDto<RutaActionResponseDto>> CreateAsync(RutaCreateDto dto);
        Task<ResponseDto<RutaActionResponseDto>> EditAsync(RutaEditDto dto, int id);
        Task<ResponseDto<RutaActionResponseDto>> DeleteAsync(int id);
    }
}
