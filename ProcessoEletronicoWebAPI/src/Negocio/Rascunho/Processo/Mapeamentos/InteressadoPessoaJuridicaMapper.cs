using AutoMapper;
using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Negocio.Rascunho.Processo.Mapeamentos
{
    public class InteressadoPessoaJuridicaMapper : Profile
    {
        public InteressadoPessoaJuridicaMapper()
        {
            /*
            CreateMap<InteressadoPessoaJuridicaModeloNegocio, InteressadoPessoaJuridicaRascunho>()
                .ForMember(dest => dest.GuidMunicipio, opt => opt.MapFrom(src => src.GuidMunicipio ? new Guid(src.GuidMunicipio)))
                .ForMember(dest => dest.ContatosRascunho, opt => opt.MapFrom(src => src.Contatos))
                .ForMember(dest => dest.EmailsRascunho, opt => opt.MapFrom(src => src.Emails));

            CreateMap<InteressadoPessoaJuridicaRascunho, InteressadoPessoaJuridicaModeloNegocio>()
                .ForMember(dest => dest.GuidMunicipio, opt => opt.MapFrom(src => src.GuidMunicipio.ToString("D")))
                .ForMember(dest => dest.Contatos, opt => opt.MapFrom(s => s.Contatos != null ? Mapper.Map<List<Contato>, List<ContatoModeloNegocio>>(s.Contatos.ToList()) : null))
                .ForMember(dest => dest.Emails, opt => opt.MapFrom(s => s.Emails != null ? Mapper.Map<List<Email>, List<EmailModeloNegocio>>(s.Emails.ToList()) : null));
    */
        }
    }
}
