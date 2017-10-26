using Apresentacao.WebAPI.Models;
using AutoMapper;
using Negocio.RascunhosDespacho.Models;

namespace Apresentacao.WebAPI.Mappings
{
    public class RascunhoDespachoMapping : Profile
    {
        public RascunhoDespachoMapping()
        {
            CreateMap<PostRascunhoDespachoDto, RascunhoDespachoModel>()
                .ForMember(dest => dest.Anexos, opt => opt.MapFrom(src => src.Anexos));

            CreateMap<RascunhoDespachoModel, GetRascunhoDespachoDto>()
                .ForMember(dest => dest.Anexos, opt => opt.MapFrom(src => src.Anexos));

            CreateMap<RascunhoDespachoModel, PatchRascunhoDespachoDto>().ReverseMap();
        }
    }
}
