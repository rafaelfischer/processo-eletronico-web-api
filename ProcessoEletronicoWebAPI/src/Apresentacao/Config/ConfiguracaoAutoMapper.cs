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
            CreateMap<AtividadeModeloNegocio, AtividadeProcessoGetModelo>();

            CreateMap<AtividadeModelo, AtividadeModeloNegocio>();
            #endregion

            #region Mapeamento de Email
            CreateMap<EmailModelo, EmailModeloNegocio>();

            CreateMap<EmailModeloNegocio, EmailModelo>();
            #endregion

            #region Mapeamento de Contato
            CreateMap<ContatoModelo, ContatoModeloNegocio>();

            CreateMap<ContatoModeloNegocio, ContatoProcessoGetModelo>();
            #endregion

            #region Mapeamento de despacho
            CreateMap<DespachoModeloNegocio, DespachoProcessoGetModelo>();
            #endregion

            #region Mapeamento de função
            CreateMap<FuncaoModeloNegocio, FuncaoModelo>()
                .ForMember(dest => dest.IdPlanoClassificacao, opt => opt.MapFrom(src => src.PlanoClassificacao.Id))
                .ForMember(dest => dest.IdFuncaoPai, opt => opt.MapFrom(src => src.FuncaoPai != null ? src.FuncaoPai.Id : (int?)null));

            CreateMap<FuncaoModeloNegocio, FuncaoProcessoGetModelo>();
            #endregion

            #region Mapeamento de Interessados Pessoa Física
            CreateMap<InteressadoPessoaFisicaModelo, InteressadoPessoaFisicaModeloNegocio>()
                .ForMember(dest => dest.Contatos, opt => opt.MapFrom(src => src.Contatos))
                .ForMember(dest => dest.Emails, opt => opt.MapFrom(src => src.Emails))
                .ForMember(dest => dest.NomeMunicipio, opt => opt.MapFrom(src => src.Municipio));

            CreateMap<InteressadoPessoaFisicaModeloNegocio, InteressadoPessoaFisicaProcessoGetModelo>();
            #endregion

            #region Mapeamento de Interessados  Pessoa Jurídica
            CreateMap<InteressadoPessoaJuridicaModelo, InteressadoPessoaJuridicaModeloNegocio>()
                .ForMember(dest => dest.Contatos, opt => opt.MapFrom(src => src.Contatos))
                .ForMember(dest => dest.Emails, opt => opt.MapFrom(src => src.Emails))
                .ForMember(dest => dest.NomeMunicipio, opt => opt.MapFrom(src => src.Municipio));

            CreateMap<InteressadoPessoaJuridicaModeloNegocio, InteressadoPessoaJuridicaProcessoGetModelo>();
            #endregion

            #region Mapeamento de Municipio
            CreateMap<MunicipioProcessoModelo, MunicipioProcessoModeloNegocio>();

            CreateMap<MunicipioProcessoModeloNegocio, MunicipioProcessoModelo>();
            #endregion

            #region Mapeamento de plano de classificação
            CreateMap<PlanoClassificacaoModeloNegocio, PlanoClassificacaoModelo>()
                .ForMember(dest => dest.IdOrganizacaoProcesso, opt => opt.MapFrom(src => src.OrganizacaoProcesso.IdOrganizacao));

            CreateMap<PlanoClassificacaoModeloNegocio, PlanoClassificacaoProcessoGetModelo>();
            #endregion

            #region Mapeamento de Processo
            CreateMap<ProcessoModeloPost, ProcessoModeloNegocio>()
                .ForMember(dest => dest.Atividade, opt => opt.MapFrom(src => new AtividadeModeloNegocio { Id = src.IdAtividade }))
                .ForMember(dest => dest.InteressadoPessoaFisica, opt => opt.MapFrom(src => Mapper.Map<List<InteressadoPessoaFisicaModelo>, List<InteressadoPessoaFisicaModeloNegocio>>(src.InteressadosPessoaFisica)))
                .ForMember(dest => dest.InteressadoPessoaJuridica, opt => opt.MapFrom(src => Mapper.Map<List<InteressadoPessoaJuridicaModelo>, List<InteressadoPessoaJuridicaModeloNegocio>>(src.InteressadosPessoaJuridica)))
                .ForMember(dest => dest.Sinalizacao, opt => opt.MapFrom(src => src.IdSinalizacoes))
                .ForMember(dest => dest.Anexo, opt => opt.Ignore())
                .ForMember(dest => dest.MunicipioProcesso, opt => opt.MapFrom(src => src.Municipios));

            CreateMap<ProcessoModeloNegocio, ProcessoModelo>()
                .ForMember(dest => dest.DataAutuacao, opt => opt.MapFrom(src => src.DataAutuacao.ToString("dd/MM/yyyy HH:mm:ss")))
                .ForMember(dest => dest.IdAtividade, opt => opt.MapFrom(src => src.Atividade.Id))
                .ForMember(dest => dest.IdOrganizacaoProcesso, opt => opt.MapFrom(src => src.OrganizacaoProcesso.IdOrganizacao));

            CreateMap<ProcessoModeloNegocio, ProcessoCompletoModelo>()
                .ForMember(dest => dest.DataAutuacao, opt => opt.MapFrom(src => src.DataAutuacao.ToString("dd/MM/yyyy HH:mm:ss")))
                //.ForMember(dest => dest.IdAtividade, opt => opt.MapFrom(src => src.Atividade.Id))
                .ForMember(dest => dest.IdOrganizacaoProcesso, opt => opt.MapFrom(src => src.OrganizacaoProcesso.IdOrganizacao));
            #endregion

            #region Mapeamento de Tipo de Contato

            CreateMap<TipoContatoModeloNegocio, TipoContatoModelo>();

            CreateMap<TipoContatoModeloNegocio, TipoContatoProcessoGetModelo>();

            #endregion

            #region Mapeamento de Tipo Documental

            CreateMap<TipoDocumentalModeloNegocio, TipoDocumentalModelo>().
                ForMember(dest => dest.IdAtividade, opt => opt.MapFrom(src => src.Atividade.Id));

            #endregion

            #region Mapeamento de Sinalização
            CreateMap<int, SinalizacaoModeloNegocio>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src));

            CreateMap<SinalizacaoModeloNegocio, SinalizacaoProcessoGetModelo>();
            #endregion


        }

    }
}




