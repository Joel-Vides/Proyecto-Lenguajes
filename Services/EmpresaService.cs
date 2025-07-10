using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Terminal.Constants;
using Terminal.Database;
using Terminal.Database.Entities;
using Terminal.Dtos.Common;
using Terminal.Dtos.Empresa;
using Terminal.Services.Interfaces;
using HttpStatusCode = System.Net.HttpStatusCode;

namespace Terminal.Services
{
    public class EmpresaService : IEmpresaService
    {
        private readonly TerminalDbContext _context;
        private readonly IMapper _mapper;
        private readonly int _defaultPageSize;

        public EmpresaService(
            TerminalDbContext context,
            IMapper mapper,
            IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _defaultPageSize = configuration.GetValue<int>("Pagination:PageSize");
        }

        public async Task<ResponseDto<PaginationDto<List<EmpresaDto>>>> GetListAsync(
            string searchTerm = "", int page = 1, int pageSize = 0)
        {
            pageSize = pageSize == 0 ? _defaultPageSize : pageSize;

            try
            {
                IQueryable<EmpresaEntity> query = _context.Empresas.AsQueryable();

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    query = query.Where(e =>
                        (e.Name + " " + e.Email + " " + e.PhoneNumber).Contains(searchTerm));
                }

                int totalRows = await query.CountAsync();
                var empresas = await query
                    .OrderBy(e => e.Name)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return new ResponseDto<PaginationDto<List<EmpresaDto>>>
                {
                    StatusCode = Constants.HttpStatusCode.OK,
                    Status = true,
                    Message = empresas.Any() ? "Empresas encontradas" : "No hay registros",
                    Data = new PaginationDto<List<EmpresaDto>>
                    {
                        CurrentPage = page,
                        PageSize = pageSize,
                        TotalItems = totalRows,
                        TotalPages = (int)Math.Ceiling(totalRows / (double)pageSize),
                        Items = _mapper.Map<List<EmpresaDto>>(empresas),
                        HasNextPage = page * pageSize < totalRows,
                        HasPreviousPage = page > 1
                    }
                };
            }
            catch
            {
                return new ResponseDto<PaginationDto<List<EmpresaDto>>>
                {
                    StatusCode = Constants.HttpStatusCode.BAD_REQUEST,
                    Status = false,
                    Message = "Error al obtener empresas"
                };
            }
        }

        public async Task<ResponseDto<EmpresaDto>> GetOneByIdAsync(string id)
        {
            try
            {
                var empresa = await _context.Empresas.FindAsync(id);

                if (empresa == null)
                    return new ResponseDto<EmpresaDto>
                    {
                        StatusCode = Constants.HttpStatusCode.BAD_REQUEST,
                        Status = false,
                        Message = "Empresa no encontrada"
                    };

                return new ResponseDto<EmpresaDto>
                {
                    StatusCode = Constants.HttpStatusCode.OK,
                    Status = true,
                    Message = "Empresa encontrada",
                    Data = _mapper.Map<EmpresaDto>(empresa)
                };
            }
            catch
            {
                return new ResponseDto<EmpresaDto>
                {
                    StatusCode = Constants.HttpStatusCode.BAD_REQUEST,
                    Status = false,
                    Message = "Error al obtener empresa"
                };
            }
        }

        public async Task<ResponseDto<EmpresaActionResponseDto>> CreateAsync(EmpresaCreateDto dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                if (await _context.Empresas.AnyAsync(e => e.Email == dto.Email))
                    return new ResponseDto<EmpresaActionResponseDto>
                    {
                        StatusCode = Constants.HttpStatusCode.BAD_REQUEST,
                        Status = false,
                        Message = "El email ya está registrado"
                    };

                var empresa = _mapper.Map<EmpresaEntity>(dto);
                empresa.Id = Guid.NewGuid().ToString();

                _context.Empresas.Add(empresa);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return new ResponseDto<EmpresaActionResponseDto>
                {
                    StatusCode = Constants.HttpStatusCode.OK,
                    Status = true,
                    Message = "Empresa creada exitosamente",
                    Data = _mapper.Map<EmpresaActionResponseDto>(empresa)
                };
            }
            catch
            {
                await transaction.RollbackAsync();
                return new ResponseDto<EmpresaActionResponseDto>
                {
                    StatusCode = Constants.HttpStatusCode.INTERNAL_SERVER_ERROR,
                    Status = false,
                    Message = "Error al crear empresa"
                };
            }
        }

        public async Task<ResponseDto<EmpresaActionResponseDto>> UpdateAsync(string id, EmpresaDto dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var empresa = await _context.Empresas.FindAsync(id);
                if (empresa == null)
                    return new ResponseDto<EmpresaActionResponseDto>
                    {
                        StatusCode = Constants.HttpStatusCode.BAD_REQUEST,
                        Status = false,
                        Message = "Empresa no encontrada"
                    };

                _mapper.Map(dto, empresa);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return new ResponseDto<EmpresaActionResponseDto>
                {
                    StatusCode = Constants.HttpStatusCode.OK,
                    Status = true,
                    Message = "Empresa actualizada exitosamente",
                    Data = _mapper.Map<EmpresaActionResponseDto>(empresa)
                };
            }
            catch
            {
                await transaction.RollbackAsync();
                return new ResponseDto<EmpresaActionResponseDto>
                {
                    StatusCode = Constants.HttpStatusCode.INTERNAL_SERVER_ERROR,
                    Status = false,
                    Message = "Error al actualizar empresa"
                };
            }
        }

        public async Task<ResponseDto<EmpresaActionResponseDto>> DeleteAsync(string id)
        {
            try
            {
                var empresa = await _context.Empresas.FindAsync(id);
                if (empresa == null)
                    return new ResponseDto<EmpresaActionResponseDto>
                    {
                        StatusCode = Constants.HttpStatusCode.NOT_FOUND,
                        Status = false,
                        Message = "Empresa no encontrada"
                    };

                _context.Empresas.Remove(empresa);
                await _context.SaveChangesAsync();

                return new ResponseDto<EmpresaActionResponseDto>
                {
                    StatusCode = Constants.HttpStatusCode.OK,
                    Status = true,
                    Message = "Empresa eliminada exitosamente",
                    Data = _mapper.Map<EmpresaActionResponseDto>(empresa)
                };
            }
            catch
            {
                return new ResponseDto<EmpresaActionResponseDto>
                {
                    StatusCode = Constants.HttpStatusCode.BAD_REQUEST,
                    Status = false,
                    Message = "Error al eliminar empresa"
                };
            }
        }
    }
}