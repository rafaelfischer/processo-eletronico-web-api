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
                cfg.AllowNullCollections = true;
                cfg.AddProfile<ApresentacaoProfile>();
                cfg.AddProfile<NegocioProfile>();
            });
        }

    }
    public class ApresentacaoProfile : Profile
    {

        public ApresentacaoProfile()
        {
            #region Mapeamento de atividade
            CreateMap<AtividadeModeloNegocio, AtividadeModelo>()
                .ForMember(dest => dest.IdFuncao, opt => opt.MapFrom(src => src.Funcao.Id));
            #endregion

            #region Mapeamento de função
            CreateMap<FuncaoModeloNegocio, FuncaoModelo>()
                .ForMember(dest => dest.IdPlanoClassificacao, opt => opt.MapFrom(src => src.PlanoClassificacao.Id))
                .ForMember(dest => dest.IdFuncaoPai, opt => opt.MapFrom(src => src.FuncaoPai != null ? src.FuncaoPai.Id : (int?)null));
            #endregion

           
            #region Mapeamento de plano de classificação
            CreateMap<PlanoClassificacaoModeloNegocio, PlanoClassificacaoModelo>()
                .ForMember(dest => dest.IdOrganizacaoProcesso, opt => opt.MapFrom(src => src.OrganizacaoProcesso.IdOrganizacao));
            #endregion

            #region Mapeamento de Processo
            #endregion

            #region Mapeamento de Tipo de Contato

            CreateMap<TipoContatoModeloNegocio, TipoContatoModelo>();

            #endregion

            #region Mapeamento de Tipo Documental

            CreateMap<TipoDocumentalModeloNegocio, TipoDocumentalModelo>().
                ForMember(dest => dest.IdAtividade, opt => opt.MapFrom(src => src.Atividade.Id));

            #endregion

            #region Mapeamento de sinalização
            CreateMap<SinalizacaoModeloNegocio, SinalizacaoModelo>()
                .ForMember(dest => dest.IdOrganizacaoProcesso, opt => opt.MapFrom(src => src.OrganizacaoProcesso.IdOrganizacao));
            #endregion

        }

    }
}




