using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Terminal.Database;
using Terminal.Database.Entities;
using Terminal.Dtos.Common;
using Terminal.Dtos.Horarios;
using Terminal.Services.Interfaces;
using HttpStatusCode = Terminal.Constants.HttpStatusCode;

namespace Terminal.Services
{
    public class HorarioService : IHorarioService
    {
        private readonly TerminalDbContext _context;
        private readonly IMapper _mapper;

        public HorarioService(TerminalDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResponseDto<List<HorarioActionResponseDto>>> GetListAsync()
        {
            var horarios = await _context.Horarios
                .OrderBy(h => h.HoraSalida)
                .ToListAsync();

            var dto = _mapper.Map<List<HorarioActionResponseDto>>(horarios);

            return new ResponseDto<List<HorarioActionResponseDto>>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = "Horarios obtenidos correctamente",
                Data = dto
            };
        }

        public async Task<ResponseDto<HorarioActionResponseDto>> GetOneByIdAsync(int id)
        {
            var horario = await _context.Horarios.FindAsync(id);

            if (horario is null)
            {
                return new ResponseDto<HorarioActionResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = "Horario no encontrado"
                };
            }

            return new ResponseDto<HorarioActionResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = "Horario encontrado",
                Data = _mapper.Map<HorarioActionResponseDto>(horario)
            };
        }

        public async Task<ResponseDto<HorarioActionResponseDto>> CreateAsync(HorarioCreateDto dto)
        {
            var horario = _mapper.Map<HorarioEntity>(dto);

            _context.Horarios.Add(horario);
            await _context.SaveChangesAsync();

            return new ResponseDto<HorarioActionResponseDto>
            {
                StatusCode = HttpStatusCode.CREATED,
                Status = true,
                Message = "Horario creado correctamente",
                Data = _mapper.Map<HorarioActionResponseDto>(horario)
            };
        }

        public async Task<ResponseDto<HorarioActionResponseDto>> EditAsync(HorarioEditDto dto)
        {
            var horario = await _context.Horarios.FindAsync(dto.Id);

            if (horario is null)
            {
                return new ResponseDto<HorarioActionResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = "Horario no encontrado"
                };
            }

            _mapper.Map(dto, horario);

            _context.Horarios.Update(horario);
            await _context.SaveChangesAsync();

            return new ResponseDto<HorarioActionResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = "Horario actualizado correctamente",
                Data = _mapper.Map<HorarioActionResponseDto>(horario)
            };
        }

        public async Task<ResponseDto<HorarioActionResponseDto>> DeleteAsync(int id)
        {
            var horario = await _context.Horarios.FindAsync(id);

            if (horario is null)
            {
                return new ResponseDto<HorarioActionResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = "Horario no encontrado"
                };
            }

            _context.Horarios.Remove(horario);
            await _context.SaveChangesAsync();

            return new ResponseDto<HorarioActionResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = "Horario eliminado correctamente",
                Data = _mapper.Map<HorarioActionResponseDto>(horario)
            };
        }
    }
}
