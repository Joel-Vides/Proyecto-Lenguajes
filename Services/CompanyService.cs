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
    public class CompanyService : ICompanyService
    {
        private readonly TerminalDbContext _context;
        private readonly IMapper _mapper;
        private readonly int _defaultPageSize;

        public CompanyService(
            TerminalDbContext context,
            IMapper mapper,
            IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _defaultPageSize = configuration.GetValue<int>("Pagination:PageSize");
        }

        public async Task<ResponseDto<PaginationDto<List<CompanyDto>>>> GetListAsync(
    string searchTerm = "", int page = 1, int pageSize = 0)
        {
            // 💥 Blindaje definitivo
            pageSize = (pageSize <= 0) ? (_defaultPageSize > 0 ? _defaultPageSize : 10) : pageSize;
            page = Math.Max(page, 1);
            searchTerm = searchTerm ?? "";

            try
            {
                IQueryable<CompanyEntity> query = _context.Empresas.AsQueryable();

                if (!string.IsNullOrWhiteSpace(searchTerm))
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

                var mapped = _mapper.Map<List<CompanyDto>>(empresas);

                return new ResponseDto<PaginationDto<List<CompanyDto>>>
                {
                    StatusCode = Constants.HttpStatusCode.OK,
                    Status = true,
                    Message = mapped.Any() ? "Empresas encontradas" : "No hay registros",
                    Data = new PaginationDto<List<CompanyDto>>
                    {
                        CurrentPage = page,
                        PageSize = pageSize,
                        TotalItems = totalRows,
                        TotalPages = (int)Math.Ceiling((double)totalRows / pageSize),
                        Items = mapped,
                        HasNextPage = page * pageSize < totalRows,
                        HasPreviousPage = page > 1
                    }
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine("[GetListAsync Error]", ex); // 💥 log interno

                return new ResponseDto<PaginationDto<List<CompanyDto>>>
                {
                    StatusCode = Constants.HttpStatusCode.BAD_REQUEST,
                    Status = false,
                    Message = "Error al obtener empresas"
                };
            }
        }

        public async Task<ResponseDto<CompanyDto>> GetOneByIdAsync(string id)
        {
            try
            {
                var empresa = await _context.Empresas.FindAsync(id);

                if (empresa == null)
                    return new ResponseDto<CompanyDto>
                    {
                        StatusCode = Constants.HttpStatusCode.BAD_REQUEST,
                        Status = false,
                        Message = "Empresa no encontrada"
                    };

                return new ResponseDto<CompanyDto>
                {
                    StatusCode = Constants.HttpStatusCode.OK,
                    Status = true,
                    Message = "Empresa encontrada",
                    Data = _mapper.Map<CompanyDto>(empresa)
                };
            }
            catch
            {
                return new ResponseDto<CompanyDto>
                {
                    StatusCode = Constants.HttpStatusCode.BAD_REQUEST,
                    Status = false,
                    Message = "Error al obtener empresa"
                };
            }
        }

        public async Task<ResponseDto<CompanyActionResponseDto>> CreateAsync(CompanyCreateDto dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                if (await _context.Empresas.AnyAsync(e => e.Email == dto.Email))
                    return new ResponseDto<CompanyActionResponseDto>
                    {
                        StatusCode = Constants.HttpStatusCode.BAD_REQUEST,
                        Status = false,
                        Message = "El email ya está registrado"
                    };

                var empresa = _mapper.Map<CompanyEntity>(dto);
                empresa.Id = Guid.NewGuid().ToString();

                _context.Empresas.Add(empresa);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return new ResponseDto<CompanyActionResponseDto>
                {
                    StatusCode = Constants.HttpStatusCode.OK,
                    Status = true,
                    Message = "Empresa creada exitosamente",
                    Data = _mapper.Map<CompanyActionResponseDto>(empresa)
                };
            }
            catch
            {
                await transaction.RollbackAsync();
                return new ResponseDto<CompanyActionResponseDto>
                {
                    StatusCode = Constants.HttpStatusCode.INTERNAL_SERVER_ERROR,
                    Status = false,
                    Message = "Error al crear empresa"
                };
            }
        }

        public async Task<ResponseDto<CompanyActionResponseDto>> UpdateAsync(string id, CompanyEditDto dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var empresa = await _context.Empresas.FindAsync(id);
                if (empresa == null)
                    return new ResponseDto<CompanyActionResponseDto>
                    {
                        StatusCode = Constants.HttpStatusCode.BAD_REQUEST,
                        Status = false,
                        Message = "Empresa no encontrada"
                    };

                _mapper.Map(dto, empresa);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return new ResponseDto<CompanyActionResponseDto>
                {
                    StatusCode = Constants.HttpStatusCode.OK,
                    Status = true,
                    Message = "Empresa actualizada exitosamente",
                    Data = _mapper.Map<CompanyActionResponseDto>(empresa)
                };
            }
            catch
            {
                await transaction.RollbackAsync();
                return new ResponseDto<CompanyActionResponseDto>
                {
                    StatusCode = Constants.HttpStatusCode.INTERNAL_SERVER_ERROR,
                    Status = false,
                    Message = "Error al actualizar empresa"
                };
            }
        }

        public async Task<ResponseDto<CompanyActionResponseDto>> DeleteAsync(string id)
        {
            try
            {
                var empresa = await _context.Empresas.FindAsync(id);
                if (empresa == null)
                    return new ResponseDto<CompanyActionResponseDto>
                    {
                        StatusCode = Constants.HttpStatusCode.NOT_FOUND,
                        Status = false,
                        Message = "Empresa no encontrada"
                    };

                _context.Empresas.Remove(empresa);
                await _context.SaveChangesAsync();

                return new ResponseDto<CompanyActionResponseDto>
                {
                    StatusCode = Constants.HttpStatusCode.OK,
                    Status = true,
                    Message = "Empresa eliminada exitosamente",
                    Data = _mapper.Map<CompanyActionResponseDto>(empresa)
                };
            }
            catch
            {
                return new ResponseDto<CompanyActionResponseDto>
                {
                    StatusCode = Constants.HttpStatusCode.BAD_REQUEST,
                    Status = false,
                    Message = "Error al eliminar empresa"
                };
            }
        }
    }
}