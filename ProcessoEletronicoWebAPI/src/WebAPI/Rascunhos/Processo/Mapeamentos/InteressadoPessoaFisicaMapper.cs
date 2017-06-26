using AutoMapper;
using ProcessoEletronicoService.Negocio.Modelos;
using ProcessoEletronicoService.WebAPI.Rascunhos.Processo.Modelos;

namespace ProcessoEletronicoService.WebAPI.Rascunhos.Processo.Mapeamentos
{
    public class InteressadoPessoaFisicaMapper : Profile
    {
        public InteressadoPessoaFisicaMapper()
        {
            CreateMap<PostInteressadoPessoaFisicaDto, InteressadoPessoaFisicaModeloNegocio>()
                .ForMember(dest => dest.Contatos, opt => opt.MapFrom(src => src.Contatos))
                .ForMember(dest => dest.Emails, opt => opt.MapFrom(src => src.Emails));

            CreateMap<PatchInteressadoPessoaFisicaDto, InteressadoPessoaFisicaModeloNegocio>().ReverseMap();

            CreateMap<InteressadoPessoaFisicaModeloNegocio, GetInteressadoPessoaFisicaDto>()
                .ForMember(dest => dest.Contatos, opt => opt.MapFrom(src => src.Contatos != null && src.Contatos.Count > 0 ? src.Contatos : null))
                .ForMember(dest => dest.Emails, opt => opt.MapFrom(src => src.Emails != null && src.Emails.Count > 0 ? src.Emails : null));

            CreateMap<InteressadoPessoaFisicaModeloNegocio, GetInteressadoPessoaFisicaDto>()
               .ForMember(dest => dest.Contatos, opt => opt.MapFrom(src => src.Contatos != null && src.Contatos.Count > 0 ? src.Contatos : null))
               .ForMember(dest => dest.Emails, opt => opt.MapFrom(src => src.Emails != null && src.Emails.Count > 0 ? src.Emails : null));
        }
    }
}
