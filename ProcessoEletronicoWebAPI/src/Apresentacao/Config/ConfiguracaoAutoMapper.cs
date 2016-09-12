using AutoMapper;
using ProcessoEletronicoService.Apresentacao.Publico.Modelos;
using ProcessoEletronicoService.Infraestrutura.Repositorios.Modelos;
using ProcessoEletronicoService.Negocio.Modelos;

namespace ProcessoEletronicoService.Apresentacao.Config
{
    public static class ConfiguracaoAutoMapper
    {
        public static void CriarMapeamento()
        {
            //Negocio.Config.ConfiguracaoAutoMapper.CriarMapeamento();

            
            Mapper.Initialize(cfg =>
               {

                   /*Repositório -> Negócio*/
                   cfg.CreateMap<AssuntoRepositorio, Assunto>()
                   .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.id))
                   .ForMember(dest => dest.descricao, opt => opt.MapFrom(src => src.descricao));

                   cfg.CreateMap<OrgaoRepositorio, Orgao>()
                    .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.id))
                    .ForMember(dest => dest.sigla, opt => opt.MapFrom(src => src.sigla));

                   cfg.CreateMap<ProcessoRepositorio, ProcessoNegocio>()
                    .ForMember(dest => dest.numero, opt => opt.MapFrom(src => src.numero))
                    .ForMember(dest => dest.digito, opt => opt.MapFrom(src => src.digito))
                    .ForMember(dest => dest.resumo, opt => opt.MapFrom(src => src.resumo))
                    .ForMember(dest => dest.dataAutuacao, opt => opt.MapFrom(src => src.dataAutuacao))
                    .ForMember(dest => dest.assunto, opt => opt.MapFrom(s => Mapper.Map<AssuntoRepositorio, Assunto>(s.assunto)))
                    .ForMember(dest => dest.orgaoAutuacao, opt => opt.MapFrom(s => Mapper.Map<OrgaoRepositorio, Orgao>(s.orgaoAutuacao)));

                   /*Negócio -> Apresentação*/
                   cfg.CreateMap<ProcessoNegocio, ProcessoApresentacao>()
                    .ForMember(dest => dest.numero, opt => opt.MapFrom(src => src.numero))
                    .ForMember(dest => dest.resumo, opt => opt.MapFrom(src => src.resumo))
                    .ForMember(dest => dest.assunto, opt => opt.MapFrom(src => src.assunto.descricao))
                    .ForMember(dest => dest.dataAutuacao, opt => opt.MapFrom(src => src.dataAutuacao))
                    .ForMember(dest => dest.siglaOrgaoAutuacao, opt => opt.MapFrom(src => src.orgaoAutuacao.sigla));

               });
        }
    }
}




