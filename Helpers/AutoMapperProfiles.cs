using AutoMapper;
using Terminal.API.DTOs;
using Terminal.Database.Entities;
using Terminal.Dtos.Bus;
using Terminal.Dtos.Empresa;

namespace Terminal.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CompanyCreateDto, CompanyEntity>();
            CreateMap<CompanyEntity, CompanyDto>();
            CreateMap<CompanyEntity, CompanyActionResponseDto>();

            CreateMap<BusEntity, BusDto>();
            CreateMap<BusEntity, BusActionResponse>();
            CreateMap<BusCreateDto, BusEntity>();
            CreateMap<BusEditDto, BusEntity>();
        }
    }
}
