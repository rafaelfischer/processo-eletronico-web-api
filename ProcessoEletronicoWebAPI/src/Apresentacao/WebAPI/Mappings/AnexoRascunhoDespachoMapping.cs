using Apresentacao.WebAPI.Models;
using AutoMapper;
using Negocio.RascunhosDespacho.Models;

namespace Apresentacao.WebAPI.Mappings
{
    public class AenxoRascunhoDespachoMapping : Profile
    {
        public AenxoRascunhoDespachoMapping()
        {
            CreateMap<PostRascunhoAnexoDto, AnexoRascunhoDespachoModel>()
                .ForMember(dest => dest.ConteudoString, opt => opt.MapFrom(src => src.Conteudo))
                .ForMember(dest => dest.Conteudo, opt => opt.Ignore());

            CreateMap<AnexoRascunhoDespachoModel, GetRascunhoAnexoDto>()
                .ForMember(dest => dest.Conteudo, opt => opt.MapFrom(src => src.ConteudoString));

            CreateMap<PatchRascunhoAnexoDto, AnexoRascunhoDespachoModel>()
                .ForMember(dest => dest.ConteudoString, opt => opt.MapFrom(src => src.Conteudo))
                .ForMember(dest => dest.Conteudo, opt => opt.Ignore());

            CreateMap<AnexoRascunhoDespachoModel, PatchRascunhoAnexoDto>()
                .ForMember(dest => dest.Conteudo, opt => opt.MapFrom(src => src.ConteudoString));
        }
    }
}
