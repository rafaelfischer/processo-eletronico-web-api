using AutoMapper;
using ProcessoEletronicoService.Apresentacao.Modelos;
using ProcessoEletronicoService.Negocio.Modelos;
using ProcessoEletronicoService.WebAPI.Rascunhos.Processo.Modelos;

namespace ProcessoEletronicoService.WebAPI.Rascunhos.Processo.Mapeamentos
{
    public class InteressadoPessoaJuridicaMapper : Profile
    {
        public InteressadoPessoaJuridicaMapper()
        {
            CreateMap<PostInteressadoPessoaJuridicaDto, InteressadoPessoaJuridicaModeloNegocio>()
                .ForMember(dest => dest.Contatos, opt => opt.MapFrom(src => src.Contatos))
                .ForMember(dest => dest.Emails, opt => opt.MapFrom(src => src.Emails));

            CreateMap<PatchInteressadoPessoaJuridicaDto, InteressadoPessoaJuridicaModeloNegocio>().ReverseMap();

            CreateMap<InteressadoPessoaJuridicaModeloNegocio, GetInteressadoPessoaJuridicaDto>()
                .ForMember(dest => dest.Contatos, opt => opt.MapFrom(src => src.Contatos != null && src.Contatos.Count > 0 ? src.Contatos : null))
                .ForMember(dest => dest.Emails, opt => opt.MapFrom(src => src.Emails != null && src.Emails.Count > 0 ? src.Emails : null));

            CreateMap<InteressadoPessoaJuridicaModeloNegocio, GetInteressadoPessoaJuridicaDto>()
               .ForMember(dest => dest.Contatos, opt => opt.MapFrom(src => src.Contatos != null && src.Contatos.Count > 0 ? src.Contatos : null))
               .ForMember(dest => dest.Emails, opt => opt.MapFrom(src => src.Emails != null && src.Emails.Count > 0 ? src.Emails : null));
        }
    }
}
