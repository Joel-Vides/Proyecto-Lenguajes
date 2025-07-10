using Terminal.Dtos.Common;
using Terminal.Dtos.Empresa;

namespace Terminal.Services.Interfaces
{
    public interface IEmpresaService
    {
        Task<ResponseDto<EmpresaActionResponseDto>> CreateAsync(EmpresaCreateDto dto);
        Task<ResponseDto<EmpresaDto>> GetOneByIdAsync(string id);
        Task<IEnumerable<EmpresaDto>> GetListAsync();
        Task<ResponseDto<EmpresaActionResponseDto>> UpdateAsync(string id, EmpresaCreateDto dto);
        Task<ResponseDto<EmpresaActionResponseDto>> DeleteAsync(string id);
        Task<ResponseDto<PaginationDto<List<EmpresaDto>>>> GetListAsync(string searchTerm = "", int page = 1, int pageSize = 0);
    }
}
