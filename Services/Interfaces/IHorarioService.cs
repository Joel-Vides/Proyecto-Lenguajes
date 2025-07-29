using Terminal.Dtos.Common;
using Terminal.Dtos.Horarios;

namespace Terminal.Services.Interfaces
{
    public interface IHorarioService
    {
        Task<ResponseDto<List<HorarioActionResponseDto>>> GetListAsync();
        Task<ResponseDto<HorarioActionResponseDto>> GetOneByIdAsync(int id);
        Task<ResponseDto<HorarioActionResponseDto>> CreateAsync(HorarioCreateDto dto);
        Task<ResponseDto<HorarioActionResponseDto>> EditAsync(HorarioEditDto dto);
        Task<ResponseDto<HorarioActionResponseDto>> DeleteAsync(int id);
    }
}
