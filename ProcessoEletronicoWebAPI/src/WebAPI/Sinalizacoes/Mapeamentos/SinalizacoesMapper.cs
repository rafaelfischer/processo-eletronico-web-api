using AutoMapper;
using ProcessoEletronicoService.Negocio.Modelos;
using ProcessoEletronicoService.WebAPI.Sinalizacoes.Modelos;

namespace WebAPI.Sinalizacoes.Mapeamentos
{
    public class SinalizacoesMapper : Profile
    {
        public SinalizacoesMapper()
        {
            CreateMap<SinalizacaoModeloNegocio, GetSinalizacaoDto>()
                .ForMember(dest => dest.Imagem, opt => opt.MapFrom(src => src.ImagemBase64String));

            CreateMap<PostSinalizacaoDto, SinalizacaoModeloNegocio>()
                .ForMember(dest => dest.ImagemBase64String, opt => opt.MapFrom(src => src.Imagem))
                .ForMember(dest => dest.Imagem, opt => opt.Ignore());

            CreateMap<PatchSinalizacaoDto, SinalizacaoModeloNegocio>()
                .ForMember(dest => dest.ImagemBase64String, opt => opt.MapFrom(src => src.Imagem))
                .ForMember(dest => dest.Imagem, opt => opt.Ignore());

            CreateMap<SinalizacaoModeloNegocio, PatchSinalizacaoDto>()
                .ForMember(dest => dest.Imagem, opt => opt.MapFrom(src => src.ImagemBase64String));
        }
    }
}
