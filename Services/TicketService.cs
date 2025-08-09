using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Terminal.API.Database.Entities;
using Terminal.API.Dtos.Tickets;
using Terminal.API.Services.Interfaces;
using Terminal.Constants;
using Terminal.Database;
using Terminal.Dtos.Common;
using Terminal.Dtos.Ticket;
using Terminal.Services.Interfaces;

namespace Terminal.API.Services
{
    public class TicketService : ITicketService
    {
        private readonly TerminalDbContext _context;
        private readonly IMapper _mapper;
        private readonly int PAGE_SIZE;
        private readonly int PAGE_SIZE_LIMIT;

        public TicketService(
            TerminalDbContext context,
            IMapper mapper,
            IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            PAGE_SIZE = configuration.GetValue<int>("PageSize");
            PAGE_SIZE_LIMIT = configuration.GetValue<int>("PageSizeLimit");
        }

        public async Task<ResponseDto<PaginationDto<List<TicketDto>>>> GetListAsync(
            string searchTerm = "", int page = 1, int pageSize = 0)
        {
            pageSize = pageSize == 0 ? PAGE_SIZE : pageSize;
            int startIndex = (page - 1) * pageSize;

            IQueryable<TicketEntity> ticketsQuery = _context.Tickets;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                ticketsQuery = ticketsQuery
                    .Where(t => t.NumeroTicket.Contains(searchTerm));
                             //|| t.SitioSalida.Contains(searchTerm));
            }

            int totalRows = await ticketsQuery.CountAsync();

            var ticketEntities = await ticketsQuery
                .OrderByDescending(t => t.FechaEmision)
                .Skip(startIndex)
                .Take(pageSize)
                .ToListAsync();

            var ticketDtos = _mapper.Map<List<TicketDto>>(ticketEntities);

            return new ResponseDto<PaginationDto<List<TicketDto>>>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = "Registros obtenidos correctamente",
                Data = new PaginationDto<List<TicketDto>>
                {
                    CurrentPage = page,
                    PageSize = pageSize,
                    TotalItems = totalRows,
                    TotalPages = (int)Math.Ceiling((double)totalRows / pageSize),
                    Items = ticketDtos,
                    HasNextPage = startIndex + pageSize < PAGE_SIZE_LIMIT && page < (int)Math.Ceiling((double)totalRows / pageSize),
                    HasPreviousPage = page > 1
                }
            };
        }

        public async Task<ResponseDto<TicketDto>> GetOneByIdAsync(string id)
        {
            var ticket = await _context.Tickets.FirstOrDefaultAsync(x => x.Id == id);

            if (ticket is null)
            {
                return new ResponseDto<TicketDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = "Ticket no encontrado"
                };
            }

            return new ResponseDto<TicketDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = "Ticket encontrado",
                Data = _mapper.Map<TicketDto>(ticket)
            };
        }

        public async Task<ResponseDto<TicketActionResponseDto>> CreateAsync(TicketCreateDto dto)
        {
            var entity = _mapper.Map<TicketEntity>(dto);
            entity.Id = Guid.NewGuid().ToString();

            _context.Tickets.Add(entity);
            await _context.SaveChangesAsync();

            return new ResponseDto<TicketActionResponseDto>
            {
                StatusCode = HttpStatusCode.CREATED,
                Status = true,
                Message = "Ticket creado correctamente",
                Data = _mapper.Map<TicketActionResponseDto>(entity)
            };
        }

        public async Task<ResponseDto<TicketActionResponseDto>> EditAsync(TicketCreateDto dto, string id)
        {
            var entity = await _context.Tickets.FindAsync(id);

            if (entity is null)
            {
                return new ResponseDto<TicketActionResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = "Ticket no encontrado"
                };
            }

            _mapper.Map(dto, entity);

            _context.Tickets.Update(entity);
            await _context.SaveChangesAsync();

            return new ResponseDto<TicketActionResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = "Ticket editado correctamente",
                Data = _mapper.Map<TicketActionResponseDto>(entity)
            };
        }

        public async Task<ResponseDto<TicketActionResponseDto>> DeleteAsync(string id)
        {
            var entity = await _context.Tickets.FindAsync(id);

            if (entity is null)
            {
                return new ResponseDto<TicketActionResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = "Ticket no encontrado"
                };
            }

            _context.Tickets.Remove(entity);
            await _context.SaveChangesAsync();

            return new ResponseDto<TicketActionResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = "Ticket eliminado correctamente",
                Data = _mapper.Map<TicketActionResponseDto>(entity)
            };
        }
    }
}
