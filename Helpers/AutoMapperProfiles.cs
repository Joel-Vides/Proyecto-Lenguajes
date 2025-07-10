using AutoMapper;
using Terminal.Database.Entities;
using Terminal.Dtos.Empresa;

namespace Terminal.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<EmpresaCreateDto, EmpresaEntity>();
            CreateMap<EmpresaEntity, EmpresaDto>();
            CreateMap<EmpresaEntity, EmpresaActionResponseDto>();
        }
    }
}
