using AutoMapper;
using ProcessoEletronicoService.Apresentacao.Modelos;
using ProcessoEletronicoService.Infraestrutura.Repositorios.Modelos;
using ProcessoEletronicoService.Negocio.Modelos;
using ProcessoEletronicoService.Negocio.Config;

namespace ProcessoEletronicoService.Apresentacao.Config
{
    public static class ConfiguracaoAutoMapper
    {
        public static ApresentacaoProfile GetApresentacaoProfile()
        {
            return new ApresentacaoProfile();
        }

        public static void ExecutaMapeamento()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<ApresentacaoProfile>();
                cfg.AddProfile<NegocioProfile>();
            });
        }

    }
    public class ApresentacaoProfile : Profile
    {

        public ApresentacaoProfile()
        {
            #region Mapeamento de Assunto
            CreateMap<AssuntoRepositorio, Assunto>()
                  .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.id))
                   .ForMember(dest => dest.descricao, opt => opt.MapFrom(src => src.descricao));
            #endregion

            #region Mapeamento de atividade
            CreateMap<AtividadeModeloNegocio, AtividadeModelo>()
                .ForMember(dest => dest.IdFuncao, opt => opt.MapFrom(src => src.Funcao.Id));
            #endregion

            #region Mapeamento de função
            CreateMap<FuncaoModeloNegocio, FuncaoModelo>()
                .ForMember(dest => dest.IdPlanoClassificacao, opt => opt.MapFrom(src => src.PlanoClassificacao.Id))
                .ForMember(dest => dest.IdFuncaoPai, opt => opt.MapFrom(src => src.FuncaoPai != null ? src.FuncaoPai.Id : (int?)null));
            #endregion

            #region Mapeamento de Orgao
            CreateMap<OrgaoRepositorio, Orgao>()
                    .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.id))
                    .ForMember(dest => dest.sigla, opt => opt.MapFrom(src => src.sigla));
            #endregion

            #region Mapeamento de plano de classificação
            CreateMap<PlanoClassificacaoModeloNegocio, PlanoClassificacaoModelo>()
                .ForMember(dest => dest.IdOrganizacaoProcesso, opt => opt.MapFrom(src => src.OrganizacaoProcesso.IdOrganizacao));
            #endregion

            #region Mapeamento de Processo
            CreateMap<ProcessoRepositorio, ProcessoNegocio>()
                    .ForMember(dest => dest.numero, opt => opt.MapFrom(src => src.numero))
                    .ForMember(dest => dest.digito, opt => opt.MapFrom(src => src.digito))
                    .ForMember(dest => dest.resumo, opt => opt.MapFrom(src => src.resumo))
                    .ForMember(dest => dest.dataAutuacao, opt => opt.MapFrom(src => src.dataAutuacao))
                    .ForMember(dest => dest.assunto, opt => opt.MapFrom(s => Mapper.Map<AssuntoRepositorio, Assunto>(s.assunto)))
                    .ForMember(dest => dest.orgaoAutuacao, opt => opt.MapFrom(s => Mapper.Map<OrgaoRepositorio, Orgao>(s.orgaoAutuacao)));
            #endregion

            #region Mapeamento de Tipo de Contato

            CreateMap<TipoContatoModeloNegocio, TipoContatoModelo>();

            #endregion

            #region Mapeamento de Tipo Documental

            CreateMap<TipoDocumentalModeloNegocio, TipoDocumentalModelo>().
                ForMember(dest => dest.IdAtividade, opt => opt.MapFrom(src => src.Atividade.Id));

            #endregion


        }

    }
}




