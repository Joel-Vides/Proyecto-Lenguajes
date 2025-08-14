using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Terminal.Helpers;
using Terminal.Database;
using Terminal.Database.Entities;
using Terminal.Dtos.Common;
using Terminal.Dtos.Ruta;
using Terminal.Services.Interfaces;
using Terminal.Constants;

namespace Terminal.Services
{
    public class RutaService : IRutaService
    {
        private readonly TerminalDbContext _context;
        private readonly IMapper _mapper;
        private readonly int PAGE_SIZE;
        private readonly int PAGE_SIZE_LIMIT;

        public RutaService(
            TerminalDbContext context,
            IMapper mapper,
            IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            PAGE_SIZE = configuration.GetValue<int>("PageSize");
            PAGE_SIZE_LIMIT = configuration.GetValue<int>("PageSizeLimit");
        }

        public async Task<ResponseDto<PaginationDto<List<RutaDto>>>> GetListAsync(
            string searchTerm = "", int page = 1, int pageSize = 0)
        {
            pageSize = pageSize == 0 ? PAGE_SIZE : pageSize;
            int startIndex = (page - 1) * pageSize;

            IQueryable<RutaEntity> rutaQuery = _context.Rutas;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                rutaQuery = rutaQuery.Where(x =>
                    (x.SitioSalida + " " + x.SitioDestino)
                    .Contains(searchTerm));
            }

            int totalRows = await rutaQuery.CountAsync();

            var rutasEntity = await rutaQuery
                .OrderBy(x => x.SitioSalida)
                .Skip(startIndex)
                .Take(pageSize)
                .ToListAsync();

            var rutasDto = _mapper.Map<List<RutaDto>>(rutasEntity);

            return new ResponseDto<PaginationDto<List<RutaDto>>>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = rutasEntity.Count > 0
                    ? "Registros encontrados"
                    : "No se encontraron registros",
                Data = new PaginationDto<List<RutaDto>>
                {
                    CurrentPage = page,
                    PageSize = pageSize,
                    TotalItems = totalRows,
                    TotalPages = (int)Math.Ceiling((double)totalRows / pageSize),
                    Items = rutasDto,
                    HasNextPage = startIndex + pageSize < PAGE_SIZE_LIMIT && page < (int)Math.Ceiling((double)totalRows / pageSize),
                    HasPreviousPage = page > 1
                }
            };
        }

        public async Task<ResponseDto<RutaDto>> GetOneByIdAsync(int id)
        {
            var rutaEntity = await _context.Rutas
                .FirstOrDefaultAsync(r => r.Id == id);

            if (rutaEntity is null)
            {
                return new ResponseDto<RutaDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = "Registro no encontrado"
                };
            }

            return new ResponseDto<RutaDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = "Registro encontrado",
                Data = _mapper.Map<RutaDto>(rutaEntity)
            };
        }

        public async Task<ResponseDto<RutaActionResponseDto>> CreateAsync(RutaCreateDto dto)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var rutaEntity = _mapper.Map<RutaEntity>(dto);

                    _context.Rutas.Add(rutaEntity);
                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();

                    return new ResponseDto<RutaActionResponseDto>
                    {
                        StatusCode = HttpStatusCode.CREATED,
                        Status = true,
                        Message = "Ruta creada correctamente",
                        Data = _mapper.Map<RutaActionResponseDto>(rutaEntity)
                    };
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    await transaction.RollbackAsync();

                    return new ResponseDto<RutaActionResponseDto>
                    {
                        StatusCode = HttpStatusCode.INTERNAL_SERVER_ERROR,
                        Status = false,
                        Message = "Error interno en el servidor, contacte al administrador."
                    };
                }
            }
        }

        public async Task<ResponseDto<RutaActionResponseDto>> EditAsync(RutaEditDto dto, int id)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var rutaEntity = await _context.Rutas
                        .FirstOrDefaultAsync(x => x.Id == id);

                    if (rutaEntity is null)
                    {
                        return new ResponseDto<RutaActionResponseDto>
                        {
                            StatusCode = HttpStatusCode.NOT_FOUND,
                            Status = false,
                            Message = "Registro no encontrado"
                        };
                    }

                    _mapper.Map<RutaEditDto, RutaEntity>(dto, rutaEntity);

                    _context.Rutas.Update(rutaEntity);
                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();

                    return new ResponseDto<RutaActionResponseDto>
                    {
                        StatusCode = HttpStatusCode.OK,
                        Status = true,
                        Message = "Ruta editada correctamente",
                        Data = _mapper.Map<RutaActionResponseDto>(rutaEntity)
                    };
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();

                    return new ResponseDto<RutaActionResponseDto>
                    {
                        StatusCode = HttpStatusCode.INTERNAL_SERVER_ERROR,
                        Status = false,
                        Message = "Se produjo un error al editar el registro"
                    };
                }
            }
        }

        public async Task<ResponseDto<RutaActionResponseDto>> DeleteAsync(int id)
        {
            var rutaEntity = await _context.Rutas
                .FirstOrDefaultAsync(x => x.Id == id);

            if (rutaEntity is null)
            {
                return new ResponseDto<RutaActionResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = "Registro no encontrado"
                };
            }

            _context.Rutas.Remove(rutaEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<RutaActionResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = "Ruta eliminada correctamente",
                Data = _mapper.Map<RutaActionResponseDto>(rutaEntity)
            };
        }
    }
}
