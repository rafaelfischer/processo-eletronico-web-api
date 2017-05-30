using AutoMapper;
using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Negocio.Rascunho.Mapeamentos
{
    public class InteressadoPessoaFisicaRascunhoMapper : Profile
    {
        public InteressadoPessoaFisicaRascunhoMapper()
        {
            CreateMap<InteressadoPessoaFisicaModeloNegocio, InteressadoPessoaFisicaRascunho>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.GuidMunicipio, opt => opt.MapFrom(src => new Guid(src.GuidMunicipio)))
                .ForMember(dest => dest.ContatosRascunho, opt => opt.MapFrom(src => src.Contatos))
                .ForMember(dest => dest.EmailsRascunho, opt => opt.MapFrom(src => src.Emails));

            CreateMap<InteressadoPessoaFisicaRascunho, InteressadoPessoaFisicaModeloNegocio>()
                .ForMember(dest => dest.GuidMunicipio, opt => opt.MapFrom(src => src.GuidMunicipio.HasValue ? src.GuidMunicipio.Value.ToString("D") : null ))
                .ForMember(dest => dest.Contatos, opt => opt.MapFrom(s => s.ContatosRascunho != null ? Mapper.Map<List<ContatoModeloNegocio>>(s.ContatosRascunho.ToList()) : null))
                .ForMember(dest => dest.Emails, opt => opt.MapFrom(s => s.EmailsRascunho != null ? Mapper.Map<List<EmailModeloNegocio>>(s.EmailsRascunho.ToList()) : null));
        }
    }
}
