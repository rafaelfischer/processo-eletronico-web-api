using AutoMapper;
using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Negocio.Mapeamentos
{
    public class InteressadoPessoaFisicaMapper : Profile
    {
        public InteressadoPessoaFisicaMapper()
        {
            CreateMap<InteressadoPessoaFisicaModeloNegocio, InteressadoPessoaFisica>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.GuidMunicipio, opt => opt.MapFrom(src => new Guid(src.GuidMunicipio)))
                .ForMember(dest => dest.Contatos, opt => opt.MapFrom(src => src.Contatos))
                .ForMember(dest => dest.Emails, opt => opt.MapFrom(src => src.Emails));

            CreateMap<InteressadoPessoaFisica, InteressadoPessoaFisicaModeloNegocio>()
                .ForMember(dest => dest.GuidMunicipio, opt => opt.MapFrom(src => src.GuidMunicipio.ToString("D")))
                .ForMember(dest => dest.Contatos, opt => opt.MapFrom(s => s.Contatos != null ? Mapper.Map<List<Contato>, List<ContatoModeloNegocio>>(s.Contatos.ToList()) : null))
                .ForMember(dest => dest.Emails, opt => opt.MapFrom(s => s.Emails != null ? Mapper.Map<List<Email>, List<EmailModeloNegocio>>(s.Emails.ToList()) : null));
        }
    }
}
