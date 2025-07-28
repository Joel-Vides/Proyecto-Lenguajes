using AutoMapper;
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

        }
    }
}
