using Terminal.Dtos.Common;
using Terminal.Dtos.Ticket;

namespace Terminal.API.Services.Interfaces
{
    public interface ITicketService
    {
        Task<ResponseDto<PaginationDto<List<TicketDto>>>> GetListAsync(
            string searchTerm = "", int page = 1, int pageSize = 0);

        Task<ResponseDto<TicketDto>> GetOneByIdAsync(string id);

        Task<ResponseDto<TicketActionResponseDto>> CreateAsync(TicketCreateDto dto);

        Task<ResponseDto<TicketActionResponseDto>> EditAsync(TicketCreateDto dto, string id);

        Task<ResponseDto<TicketActionResponseDto>> DeleteAsync(string id);
    }
}
