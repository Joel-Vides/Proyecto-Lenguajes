using AutoMapper;
using Terminal.API.Database.Entities;
using Terminal.API.Dtos.Tickets;
using Terminal.API.DTOs;
using Terminal.Database.Entities;
using Terminal.Dtos.Bus;
using Terminal.Dtos.Common;
using Terminal.Dtos.Empresa;
using Terminal.Dtos.Ruta;
using Terminal.Dtos.Ticket;

namespace Terminal.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // Empresa
            CreateMap<CompanyCreateDto, CompanyEntity>().ForMember(d => d.ImageUrl, o => o.MapFrom(s => s.ImageUrl)); ;
            CreateMap<CompanyEntity, CompanyDto>().ForMember(d => d.ImageUrl, o => o.MapFrom(s => s.ImageUrl)); ;
            CreateMap<CompanyEntity, CompanyActionResponseDto>().ForMember(d => d.ImageUrl, o => o.MapFrom(s => s.ImageUrl)); ;

            CreateMap<TicketEntity, TicketDto>();
            CreateMap<TicketEntity, TicketActionResponseDto>();
            CreateMap<TicketCreateDto, TicketEntity>();

            //RutaMapper

            CreateMap<RutaEntity, RutaDto>().ReverseMap();
            CreateMap<RutaCreateDto, RutaEntity>();
            CreateMap<RutaEditDto, RutaEntity>();
            CreateMap<RutaEntity, RutaActionResponseDto>();

            CreateMap<BusEntity, BusDto>()
            .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company.Name));

            CreateMap<CompanyEntity, CompanyDto>();




            // Bus -> BusDto
            CreateMap<BusEntity, BusDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.StartLocation, opt => opt.MapFrom(src => new LocationDto
                {
                    Latitude = src.StartLatitude,
                    Longitude = src.StartLongitude
                }))
                .ForMember(dest => dest.EndLocation, opt => opt.MapFrom(src => new LocationDto
                {
                    Latitude = src.EndLatitude,
                    Longitude = src.EndLongitude
                }))
                .ForMember(d => d.ImageUrl, o => o.MapFrom(s => s.ImageUrl));

            // Bus -> BusActionResponse
            CreateMap<BusEntity, BusActionResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.StartLocation, opt => opt.MapFrom(src => new LocationDto
                {
                    Latitude = src.StartLatitude,
                    Longitude = src.StartLongitude
                }))
                .ForMember(dest => dest.EndLocation, opt => opt.MapFrom(src => new LocationDto
                {
                    Latitude = src.EndLatitude,
                    Longitude = src.EndLongitude
                }))
                .ForMember(d => d.ImageUrl, o => o.MapFrom(s => s.ImageUrl));

            // BusCreateDto -> BusEntity
            CreateMap<BusCreateDto, BusEntity>()
                .ForMember(dest => dest.StartLatitude, opt => opt.MapFrom(src => src.StartLocation.Latitude))
                .ForMember(dest => dest.StartLongitude, opt => opt.MapFrom(src => src.StartLocation.Longitude))
                .ForMember(dest => dest.EndLatitude, opt => opt.MapFrom(src => src.EndLocation.Latitude))
                .ForMember(dest => dest.EndLongitude, opt => opt.MapFrom(src => src.EndLocation.Longitude))
                .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.CompanyId))
                .ForMember(d => d.ImageUrl, o => o.MapFrom(s => s.ImageUrl));


            // BusEditDto -> BusEntity
            CreateMap<BusEditDto, BusEntity>()
                .ForMember(dest => dest.StartLatitude, opt => opt.MapFrom(src => src.StartLocation.Latitude))
                .ForMember(dest => dest.StartLongitude, opt => opt.MapFrom(src => src.StartLocation.Longitude))
                .ForMember(dest => dest.EndLatitude, opt => opt.MapFrom(src => src.EndLocation.Latitude))
                .ForMember(dest => dest.EndLongitude, opt => opt.MapFrom(src => src.EndLocation.Longitude))
                .ForMember(d => d.ImageUrl, o => o.MapFrom(s => s.ImageUrl));
        }
    }
}