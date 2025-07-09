using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Terminal.Database;
using Terminal.Database.Entities;
using Terminal.Dtos.Empresa;
using Terminal.Services.Interfaces;

namespace Terminal.Services
{
    public class EmpresaService : IEmpresaService
    {
        private readonly TerminalDbContext _context;
        private readonly IMapper _mapper;

        public EmpresaService(TerminalDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<EmpresaActionResponseDto> CreateEmpresaAsync(EmpresaCreateDto dto)
        {
            var entity = _mapper.Map<EmpresaEntity>(dto);
            entity.Id = Guid.NewGuid().ToString(); // O el tipo que uses

            _context.Empresas.Add(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<EmpresaActionResponseDto>(entity);
        }

        public async Task<EmpresaDto> GetEmpresaByIdAsync(string id)
        {
            var entity = await _context.Empresas.FindAsync(id);
            if (entity == null) return null;

            return _mapper.Map<EmpresaDto>(entity);
        }

        public async Task<IEnumerable<EmpresaDto>> GetAllEmpresasAsync()
        {
            var entities = await _context.Empresas.ToListAsync();
            return _mapper.Map<IEnumerable<EmpresaDto>>(entities);
        }

        public async Task<EmpresaActionResponseDto> UpdateEmpresaAsync(string id, EmpresaCreateDto dto)
        {
            var entity = await _context.Empresas.FindAsync(id);
            if (entity == null) return null;

            _mapper.Map(dto, entity); // Mapea encima del objeto existente
            _context.Empresas.Update(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<EmpresaActionResponseDto>(entity);
        }

        public async Task<EmpresaActionResponseDto> DeleteEmpresaAsync(string id)
        {
            var entity = await _context.Empresas.FindAsync(id);
            if (entity == null) return null;

            _context.Empresas.Remove(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<EmpresaActionResponseDto>(entity);
        }
    }
}