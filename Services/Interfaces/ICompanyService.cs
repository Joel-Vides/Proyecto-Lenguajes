using Terminal.Dtos.Common;
using Terminal.Dtos.Empresa;

namespace Terminal.Services.Interfaces
{
    public interface ICompanyService
    {
        Task<ResponseDto<CompanyActionResponseDto>> CreateAsync(CompanyCreateDto dto);
        Task<ResponseDto<CompanyDto>> GetOneByIdAsync(string id);
        Task<ResponseDto<CompanyActionResponseDto>> UpdateAsync(string id, CompanyEditDto dto);
        Task<ResponseDto<CompanyActionResponseDto>> DeleteAsync(string id);
        Task<ResponseDto<PaginationDto<List<CompanyDto>>>> GetListAsync(string searchTerm = "", int page = 1, int pageSize = 0);
    }
}
