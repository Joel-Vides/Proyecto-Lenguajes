using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net;
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
        private readonly IMapper _mapper;

        public BusService(TerminalDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResponseDto<List<BusActionResponse>>> GetListAsync()
        {
            var buses = await _context.Buses
                .OrderBy(b => b.NumeroBus)
                .ToListAsync();

            var busesDto = _mapper.Map<List<BusActionResponse>>(buses);

            return new ResponseDto<List<BusActionResponse>>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = "Registros obtenidos correctamente",
                Data = busesDto
            };
        }

        public async Task<ResponseDto<BusActionResponse>> GetOneByIdAsync(Guid id)
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

            return new ResponseDto<BusActionResponse>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = "Registro encontrado",
                Data = _mapper.Map<BusActionResponse>(bus)
            };
        }

        public async Task<ResponseDto<BusActionResponse>> CreateAsync(BusCreateDto dto)
        {
            var busEntity = _mapper.Map<BusEntity>(dto);
            busEntity.Id = Guid.NewGuid().ToString();

            _context.Buses.Add(busEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<BusActionResponse>
            {
                StatusCode = HttpStatusCode.CREATED,
                Status = true,
                Message = "Registro creado correctamente",
                Data = _mapper.Map<BusActionResponse>(busEntity)
            };
        }

        public async Task<ResponseDto<BusActionResponse>> EditAsync(BusCreateDto dto, Guid id)
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

            _mapper.Map(dto, busEntity);

            _context.Buses.Update(busEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<BusActionResponse>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = "Registro editado correctamente",
                Data = _mapper.Map<BusActionResponse>(busEntity)
            };
        }

        public async Task<ResponseDto<BusActionResponse>> DeleteAsync(Guid id)
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

            // Validación de relaciones futuras (placeholder)
            // Por ahora no hay relaciones, pero aquí podrías validar si el bus tiene horarios, etc.

            _context.Buses.Remove(busEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<BusActionResponse>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = "Registro eliminado correctamente",
                Data = _mapper.Map<BusActionResponse>(busEntity)
            };
        }
    }
}
