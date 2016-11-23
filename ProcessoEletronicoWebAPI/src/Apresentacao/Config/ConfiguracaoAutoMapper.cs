using AutoMapper;
using ProcessoEletronicoService.Apresentacao.Modelos;
using ProcessoEletronicoService.Infraestrutura.Repositorios.Modelos;
using ProcessoEletronicoService.Negocio.Modelos;
using ProcessoEletronicoService.Negocio.Config;
using System.Collections.Generic;

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

            CreateMap<AtividadeModelo, AtividadeModeloNegocio>();
            #endregion

            #region Mapeamento de Email
            CreateMap<EmailModelo, EmailModeloNegocio>();
            #endregion

            #region Mapeamento de Contato
            CreateMap<ContatoModelo, ContatoModeloNegocio>();
            #endregion

            #region Mapeamento de função
            CreateMap<FuncaoModeloNegocio, FuncaoModelo>()
                .ForMember(dest => dest.IdPlanoClassificacao, opt => opt.MapFrom(src => src.PlanoClassificacao.Id))
                .ForMember(dest => dest.IdFuncaoPai, opt => opt.MapFrom(src => src.FuncaoPai != null ? src.FuncaoPai.Id : (int?)null));
            #endregion

            #region Mapeamento de Interessados (Pessoa Física e Jurídica)
            CreateMap<InteressadoPessoaFisicaModelo, InteressadoPessoaFisicaModeloNegocio>()
                .ForMember(dest => dest.Contatos, opt => opt.MapFrom(src => src.Contatos))
                .ForMember(dest => dest.Emails, opt => opt.MapFrom(src => src.Emails))
                .ForMember(dest => dest.Municipio, opt => opt.MapFrom(src => src.Municipio))
                ;
            CreateMap<InteressadoPessoaJuridicaModelo, InteressadoPessoaJuridicaModeloNegocio>()
                .ForMember(dest => dest.Contatos, opt => opt.MapFrom(src => src.Contatos))
                .ForMember(dest => dest.Emails, opt => opt.MapFrom(src => src.Emails))
                .ForMember(dest => dest.Municipio, opt => opt.MapFrom(src => src.Municipio));
            #endregion

            #region Mapeamento de Municipio
            CreateMap<MunicipioProcessoModelo, MunicipioProcessoModeloNegocio>();
            #endregion

            #region Mapeamento de plano de classificação
            CreateMap<PlanoClassificacaoModeloNegocio, PlanoClassificacaoModelo>()
                .ForMember(dest => dest.IdOrganizacaoProcesso, opt => opt.MapFrom(src => src.OrganizacaoProcesso.IdOrganizacao));
            #endregion

            #region Mapeamento de Processo
            CreateMap<ProcessoModeloPost, ProcessoModeloNegocio>()
                .ForMember(dest => dest.Atividade, opt => opt.MapFrom(src => new AtividadeModeloNegocio { Id = src.IdAtividade }))
                .ForMember(dest => dest.InteressadosPessoaFisica, opt => opt.MapFrom(src => Mapper.Map<List<InteressadoPessoaFisicaModelo>, List<InteressadoPessoaFisicaModeloNegocio>>(src.InteressadosPessoaFisica)))
                .ForMember(dest => dest.InteressadosPessoaJuridica, opt => opt.MapFrom(src => Mapper.Map<List<InteressadoPessoaJuridicaModelo>, List<InteressadoPessoaJuridicaModeloNegocio>>(src.InteressadosPessoaJuridica)))
                .ForMember(dest => dest.Sinalizacoes, opt => opt.MapFrom(src => src.IdSinalizacoes))
                .ForMember(dest => dest.Anexos , opt => opt.Ignore())
                .ForMember(dest => dest.Municipios, opt => opt.MapFrom(src => src.Municipios));
            CreateMap<ProcessoModeloNegocio, ProcessoModelo>()
                .ForMember(dest => dest.DataAutuacao, opt => opt.MapFrom(src => src.DataAutuacao.ToString("dd/MM/yyyy HH:mm:ss")))
                .ForMember(dest => dest.IdAtividade, opt => opt.MapFrom(src => src.Atividade.Id))
                .ForMember(dest => dest.IdOrganizacaoProcesso, opt => opt.MapFrom(src => src.OrganizacaoProcesso.IdOrganizacao));
            #endregion

            #region Mapeamento de Tipo de Contato

            CreateMap<TipoContatoModeloNegocio, TipoContatoModelo>();

            #endregion

            #region Mapeamento de Tipo Documental

            CreateMap<TipoDocumentalModeloNegocio, TipoDocumentalModelo>().
                ForMember(dest => dest.IdAtividade, opt => opt.MapFrom(src => src.Atividade.Id));

            #endregion

            #region Mapeamento de Sinalização
            CreateMap<int, SinalizacaoModeloNegocio>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src));
            #endregion
            

        }

    }
}




