using Terminal.Dtos.Common;
using Terminal.Dtos.Security.User;

namespace Terminal.Services.Interfaces
{
    public interface IUsersService
    {
        Task<ResponseDto<UserActionResponseDto>> CreateAsync(UserCreateDto dto);
        Task<ResponseDto<UserActionResponseDto>> DeleteAsync(string id);
        Task<ResponseDto<UserActionResponseDto>> EditAsync(UserEditDto dto, string id);
        Task<ResponseDto<PaginationDto<List<UserDto>>>> GetListAsync(string seachTerm = "", int page = 1, int pageSize = 0);
        Task<ResponseDto<UserDto>> GetOneByIdAsync(string id);
    }
}
