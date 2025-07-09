using Terminal.Dtos.Empresa;

namespace Terminal.Services.Interfaces
{
    public interface IEmpresaService
    {
        Task<EmpresaActionResponseDto> CreateEmpresaAsync(EmpresaCreateDto dto);
        Task<EmpresaDto> GetEmpresaByIdAsync(string id);
        Task<IEnumerable<EmpresaDto>> GetAllEmpresasAsync();
        Task<EmpresaActionResponseDto> UpdateEmpresaAsync(string id, EmpresaCreateDto dto);
        Task<EmpresaActionResponseDto> DeleteEmpresaAsync(string id);
    }
}
