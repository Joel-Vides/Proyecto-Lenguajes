using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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

        public BusService(TerminalDbContext context, IConfiguration configuration)
        {
            _context = context;
            _defaultPageSize = configuration.GetValue<int>("Pagination:PageSize");
        }

        public async Task<ResponseDto<PaginationDto<List<BusDto>>>> GetListAsync(string searchTerm = "", int page = 1, int pageSize = 0)
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

                int totalRows = await query.CountAsync();
                var buses = await query
                    .OrderBy(e => e.NumeroBus)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var mapped = buses.Select(e => new BusDto
                {
                    Id = e.Id,
                    NumeroBus = e.NumeroBus,
                    Chofer = e.Chofer,
                    Modelo = e.Modelo,
                    Anio = e.Anio,
                    StartLocation = new() { Latitude = e.StartLatitude, Longitude = e.StartLongitude },
                    EndLocation = new() { Latitude = e.EndLatitude, Longitude = e.EndLongitude }
                }).ToList();

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

            var result = new BusActionResponse
            {
                Id = bus.Id,
                NumeroBus = bus.NumeroBus,
                Chofer = bus.Chofer,
                Modelo = bus.Modelo,
                Anio = bus.Anio,
                StartLocation = new() { Latitude = bus.StartLatitude, Longitude = bus.StartLongitude },
                EndLocation = new() { Latitude = bus.EndLatitude, Longitude = bus.EndLongitude }
            };

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
            var busEntity = new BusEntity
            {
                Id = Guid.NewGuid().ToString(),
                NumeroBus = dto.NumeroBus,
                Chofer = dto.Chofer,
                Modelo = dto.Modelo,
                Anio = dto.Anio,
                StartLatitude = dto.StartLocation.Latitude,
                StartLongitude = dto.StartLocation.Longitude,
                EndLatitude = dto.EndLocation.Latitude,
                EndLongitude = dto.EndLocation.Longitude
            };

            _context.Buses.Add(busEntity);
            await _context.SaveChangesAsync();

            var result = new BusActionResponse
            {
                Id = busEntity.Id,
                NumeroBus = busEntity.NumeroBus,
                Chofer = busEntity.Chofer,
                Modelo = busEntity.Modelo,
                Anio = busEntity.Anio,
                StartLocation = new() { Latitude = busEntity.StartLatitude, Longitude = busEntity.StartLongitude },
                EndLocation = new() { Latitude = busEntity.EndLatitude, Longitude = busEntity.EndLongitude }
            };

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

            busEntity.NumeroBus = dto.NumeroBus;
            busEntity.Chofer = dto.Chofer;
            busEntity.Modelo = dto.Modelo;
            busEntity.Anio = dto.Anio;
            busEntity.StartLatitude = dto.StartLocation.Latitude;
            busEntity.StartLongitude = dto.StartLocation.Longitude;
            busEntity.EndLatitude = dto.EndLocation.Latitude;
            busEntity.EndLongitude = dto.EndLocation.Longitude;

            _context.Buses.Update(busEntity);
            await _context.SaveChangesAsync();

            var result = new BusActionResponse
            {
                Id = busEntity.Id,
                NumeroBus = busEntity.NumeroBus,
                Chofer = busEntity.Chofer,
                Modelo = busEntity.Modelo,
                Anio = busEntity.Anio,
                StartLocation = new() { Latitude = busEntity.StartLatitude, Longitude = busEntity.StartLongitude },
                EndLocation = new() { Latitude = busEntity.EndLatitude, Longitude = busEntity.EndLongitude }
            };

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

            var result = new BusActionResponse
            {
                Id = busEntity.Id,
                NumeroBus = busEntity.NumeroBus,
                Chofer = busEntity.Chofer,
                Modelo = busEntity.Modelo,
                Anio = busEntity.Anio,
                StartLocation = new() { Latitude = busEntity.StartLatitude, Longitude = busEntity.StartLongitude },
                EndLocation = new() { Latitude = busEntity.EndLatitude, Longitude = busEntity.EndLongitude }
            };

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