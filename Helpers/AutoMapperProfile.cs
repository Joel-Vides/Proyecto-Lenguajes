using AutoMapper;
using Terminal.Database.Entities;
using Terminal.Dtos.Empresa;

namespace Terminal.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<EmpresaCreateDto, EmpresaEntity>();
            CreateMap<EmpresaEntity, EmpresaDto>();
            CreateMap<EmpresaEntity, EmpresaActionResponseDto>();
        }
    }
}
