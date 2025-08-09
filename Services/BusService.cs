using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Terminal.API.DTOs;
using Terminal.Constants;
using Terminal.Database;
using Terminal.Database.Entities;
using Terminal.Dtos.Bus;
using Terminal.Dtos.Common;
using Terminal.Services.Interfaces;
using HttpStatusCode = Terminal.Constants.HttpStatusCode;

namespace Terminal.Services
{
    public class BusService : IBusService
    {
        private readonly TerminalDbContext _context;
        private readonly int _defaultPageSize;
        private readonly IMapper _mapper;

        public BusService(TerminalDbContext context, IConfiguration configuration, IMapper mapper)
        {
            _context = context;
            _defaultPageSize = configuration.GetValue<int>("Pagination:PageSize");
            _mapper = mapper;
        }

        public async Task<ResponseDto<PaginationDto<List<BusDto>>>> GetListAsync(
            string searchTerm = "",
            string companyId = "",
            int page = 1,
            int pageSize = 0)
        {
            pageSize = pageSize == 0 ? _defaultPageSize : pageSize;

            try
            {
                IQueryable<BusEntity> query = _context.Buses.AsQueryable();

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    query = query.Where(e =>
                        (e.NumeroBus + " " + e.Chofer + " " + e.Modelo + " " + e.Anio).Contains(searchTerm));
                }

                if (!string.IsNullOrWhiteSpace(companyId))
                {
                    query = query.Where(e => e.CompanyId.ToLower().Trim() == companyId.ToLower().Trim());
                }

                int totalRows = await query.CountAsync();
                var buses = await query
                    .OrderBy(e => e.NumeroBus)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var mapped = _mapper.Map<List<BusDto>>(buses);

                return new ResponseDto<PaginationDto<List<BusDto>>>
                {
                    StatusCode = HttpStatusCode.OK,
                    Status = true,
                    Message = mapped.Any() ? "Buses encontrados" : "No hay registros",
                    Data = new PaginationDto<List<BusDto>>
                    {
                        CurrentPage = page,
                        PageSize = pageSize,
                        TotalItems = totalRows,
                        TotalPages = (int)Math.Ceiling(totalRows / (double)pageSize),
                        Items = mapped,
                        HasNextPage = page * pageSize < totalRows,
                        HasPreviousPage = page > 1
                    }
                };
            }
            catch
            {
                return new ResponseDto<PaginationDto<List<BusDto>>>
                {
                    StatusCode = HttpStatusCode.BAD_REQUEST,
                    Status = false,
                    Message = "Error al obtener Buses"
                };
            }
        }

        public async Task<ResponseDto<BusActionResponse>> GetOneByIdAsync(string id)
        {
            var bus = await _context.Buses.FindAsync(id);

            if (bus is null)
            {
                return new ResponseDto<BusActionResponse>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = "El registro no fue encontrado."
                };
            }

            var result = _mapper.Map<BusActionResponse>(bus);

            return new ResponseDto<BusActionResponse>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = "Registro encontrado",
                Data = result
            };
        }

        public async Task<ResponseDto<BusActionResponse>> CreateAsync(BusCreateDto dto)
        {
            var busEntity = _mapper.Map<BusEntity>(dto);
            busEntity.Id = Guid.NewGuid().ToString();

            _context.Buses.Add(busEntity);
            await _context.SaveChangesAsync();

            var result = _mapper.Map<BusActionResponse>(busEntity);

            return new ResponseDto<BusActionResponse>
            {
                StatusCode = HttpStatusCode.CREATED,
                Status = true,
                Message = "Registro creado correctamente",
                Data = result
            };
        }

        public async Task<ResponseDto<BusActionResponse>> EditAsync(BusCreateDto dto, string id)
        {
            var busEntity = await _context.Buses.FindAsync(id);

            if (busEntity is null)
            {
                return new ResponseDto<BusActionResponse>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = "Registro no encontrado"
                };
            }

            // Guardar imagen si no sale una nueva
            var originalImageUrl = busEntity.ImageUrl;

            _mapper.Map(dto, busEntity);

            if (string.IsNullOrWhiteSpace(dto.ImageUrl))
            {
                busEntity.ImageUrl = originalImageUrl;
            }

            _context.Buses.Update(busEntity);
            await _context.SaveChangesAsync();

            var result = _mapper.Map<BusActionResponse>(busEntity);

            return new ResponseDto<BusActionResponse>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = "Registro editado correctamente",
                Data = result
            };
        }

        public async Task<ResponseDto<BusActionResponse>> DeleteAsync(string id)
        {
            var busEntity = await _context.Buses.FindAsync(id);

            if (busEntity is null)
            {
                return new ResponseDto<BusActionResponse>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = "Registro no encontrado"
                };
            }

            _context.Buses.Remove(busEntity);
            await _context.SaveChangesAsync();

            var result = _mapper.Map<BusActionResponse>(busEntity);

            return new ResponseDto<BusActionResponse>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = "Registro eliminado correctamente",
                Data = result
            };
        }
    }
}
