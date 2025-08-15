using Terminal.Dtos.Common;
using Terminal.Dtos.Security.Auth;

namespace Terminal.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseDto<LoginResponseDto>> LoginAsync(LoginDto dto);
        Task<ResponseDto<LoginResponseDto>> RefreshTokenAsync(RefreshTokenDto dto);
    }
}
